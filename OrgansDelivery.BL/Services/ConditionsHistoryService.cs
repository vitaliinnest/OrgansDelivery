using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IConditionsHistoryService
{
    Task<Result<ConditionsRecordDto>> AddConditionsRecordAsync(
        Guid containerId, CreateConditionsRecordModel model);
    Result<ConditionsRecordDto> GetConditionsRecord(Guid recordId);
    Task<Result<List<ConditionsRecordDto>>> GetConditionsHistoryAsync(
        Guid containerId, GetConditionsHistoryModel model);
    List<ConditionsViolation> GetConditionViolations(GetConditionsHistoryModel model);
}

public class ConditionsHistoryService : IConditionsHistoryService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public ConditionsHistoryService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public async Task<Result<ConditionsRecordDto>> AddConditionsRecordAsync(
        Guid containerId, CreateConditionsRecordModel model)
    {
        var containerExists = _context.Containers.Any(c => c.Id == containerId);
        if (!containerExists)
        {
            return Result.Fail("Container not found");
        }

        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var record = _mapper.Map<ConditionsRecord>(model);
        record.ContainerId = containerId;

        _context.Add(record);
        _context.SaveChanges();

        var dto = _mapper.Map<ConditionsRecordDto>(record);

        return dto;
    }

    public Result<ConditionsRecordDto> GetConditionsRecord(Guid recordId)
    {
        var record = _context.ConditionsHistory.FirstOrDefault(r => r.Id == recordId);
        if (record == null)
        {
            return Result.Fail("Record not found");
        }

        var dto = _mapper.Map<ConditionsRecordDto>(record);

        return dto;
    }

    public async Task<Result<List<ConditionsRecordDto>>> GetConditionsHistoryAsync(
        Guid containerId, GetConditionsHistoryModel model)
    {
        //var validationResult = await _genericValidator.ValidateAsync(model);
        //if (!validationResult.IsValid)
        //{
        //    return Result.Fail(validationResult.ToString());
        //}

        var containerExists = _context.Containers.Any(c => c.Id == containerId);
        if (!containerExists)
        {
            return Result.Fail("Container not found");
        }

        var history = _context.ConditionsHistory
            .Where(c => c.ContainerId == containerId && model.Start <= c.DateTime && c.DateTime <= model.End)
            .ToList();

        var dtos = _mapper.Map<List<ConditionsRecordDto>>(history);

        return dtos;
    }

    public List<ConditionsViolation> GetConditionViolations(GetConditionsHistoryModel model)
    {
        var containerByIdMap = _context.Containers
            .Include(c => c.Conditions)
            .ToDictionary(c => c.Id);

        var history = _context.ConditionsHistory
            .Where(c => model.Start <= c.DateTime && c.DateTime <= model.End)
            .ToList();

        var violations = history
            .Select(record =>
            {
                var conditions = containerByIdMap[record.ContainerId]?.Conditions
                    ?? throw new ArgumentException("Container not found");

                return new ConditionsViolation()
                {
                    ContainerId = record.ContainerId,
                    ConditionRecordId = record.Id,
                    Temperature = new()
                    {
                        ExpectedValue = conditions.Temperature.ExpectedValue,
                        AllowedDeviation = conditions.Temperature.AllowedDeviation,
                        Actual = record.Temperature,
                        IsViolated = IsViolatedDecimalCondition(record, conditions.Temperature)
                    },
                    Humidity = new()
                    {
                        ExpectedValue = conditions.Humidity.ExpectedValue,
                        AllowedDeviation = conditions.Humidity.AllowedDeviation,
                        Actual = record.Humidity,
                        IsViolated = IsViolatedDecimalCondition(record, conditions.Humidity)
                    },
                    Light = new()
                    {
                        ExpectedValue = conditions.Light.ExpectedValue,
                        AllowedDeviation = conditions.Light.AllowedDeviation,
                        Actual = record.Light,
                        IsViolated = IsViolatedDecimalCondition(record, conditions.Light)
                    },
                    Orientation = new()
                    {
                        ExpectedValue = conditions.Orientation.ExpectedValue,
                        AllowedDeviation = conditions.Orientation.AllowedDeviation,
                        Actual = record.Orientation,
                        IsViolated = IsViolatedOrientationCondition(record, conditions.Orientation)
                    }
                };
            })
            .Where(r => r.IsViolated())
            .ToList();

        return violations;
    }

    private static bool IsViolatedDecimalCondition(ConditionsRecord record, Condition<decimal> condition)
    {
        return condition.ExpectedValue - condition.AllowedDeviation > record.Temperature
            || record.Temperature > condition.ExpectedValue + condition.AllowedDeviation;
    }

    private static bool IsViolatedOrientationCondition(ConditionsRecord record, Condition<Orientation> condition)
    {
        var isViolatedX = IsViolatedDecimalCondition(record, new()
        {
            ExpectedValue = condition.ExpectedValue.X,
            AllowedDeviation = condition.AllowedDeviation.X
        });

        var isViolatedY = IsViolatedDecimalCondition(record, new()
        {
            ExpectedValue = condition.ExpectedValue.Y,
            AllowedDeviation = condition.AllowedDeviation.Y
        });

        return isViolatedX || isViolatedY;
    }
}

public interface IViolationValidator
{

}

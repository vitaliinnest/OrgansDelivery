using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IRecordsService
{
    Task<Result<ConditionsRecordDto>> AddConditionsRecordAsync(CreateConditionsRecordModel model);
    Result<ConditionsRecordDto> GetConditionsRecord(Guid recordId);
    Task<Result<List<ConditionsRecordDto>>> GetConditionsInRange(
        Guid deviceId, GetConditionsHistoryModel model);
    List<ConditionsViolation> GetConditionViolations(GetConditionsHistoryModel model);
}

public class RecordsService : IRecordsService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public RecordsService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public async Task<Result<ConditionsRecordDto>> AddConditionsRecordAsync(CreateConditionsRecordModel model)
    {
        var device = _context.Devices.IgnoreQueryFilters().FirstOrDefault(d => d.Id == model.Device_id);
        if (device == null)
        {
            return Result.Fail("Device not found");
        }

        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var record = _mapper.Map<ConditionsRecord>(model);
        record.TenantId = device.TenantId;
        record.DeviceId = device.Id;
        record.ContainerId = device.ContainerId;

		_context.Add(record);
        _context.SaveChanges();

        var dto = _mapper.Map<ConditionsRecordDto>(record);

        return dto;
    }

    public Result<ConditionsRecordDto> GetConditionsRecord(Guid recordId)
    {
        var record = _context.Records.FirstOrDefault(r => r.Id == recordId);
        if (record == null)
        {
            return Result.Fail("Record not found");
        }

        var dto = _mapper.Map<ConditionsRecordDto>(record);

        return dto;
    }

    public async Task<Result<List<ConditionsRecordDto>>> GetConditionsInRange(
        Guid deviceId, GetConditionsHistoryModel model)
    {
        // todo: add validation
        //var validationResult = await _genericValidator.ValidateAsync(model);
        //if (!validationResult.IsValid)
        //{
        //    return Result.Fail(validationResult.ToString());
        //}

        var deviceExists = _context.Devices.Any(c => c.Id == deviceId);
        if (!deviceExists)
        {
            return Result.Fail("Device not found");
        }

        var history = _context.Records
            .Where(c => c.DeviceId == deviceId &&
                model.Start.ToUniversalTime() <= c.DateTime && c.DateTime <= model.End.ToUniversalTime())
            .ToList();

        var dtos = _mapper.Map<List<ConditionsRecordDto>>(history);

        return dtos;
    }

    public List<ConditionsViolation> GetConditionViolations(GetConditionsHistoryModel model)
    {
        var containerByIdMap = _context.Containers
            .Include(c => c.Conditions)
            .ToDictionary(c => c.Id);

        var history = _context.Records
            .Where(c => model.Start <= c.DateTime && c.DateTime <= model.End)
            .ToList();

        var violations = history
            .Where(r => r.ContainerId.HasValue)
            .Select(record =>
            {
                var conditions = containerByIdMap[record.ContainerId.Value]?.Conditions
                    ?? throw new ArgumentException("Container not found");

                return new ConditionsViolation()
                {
                    RecordId = record.Id,
                    ContainerId = record.ContainerId,
                    DeviceId = record.DeviceId,
                    Temperature = new()
                    {
                        ExpectedValue = conditions.Temperature.ExpectedValue,
                        AllowedDeviation = conditions.Temperature.AllowedDeviation,
                        Actual = record.Temperature,
                        IsViolated = IsViolatedDecimalCondition(record.Temperature, conditions.Temperature)
                    },
                    Humidity = new()
                    {
                        ExpectedValue = conditions.Humidity.ExpectedValue,
                        AllowedDeviation = conditions.Humidity.AllowedDeviation,
                        Actual = record.Humidity,
                        IsViolated = IsViolatedDecimalCondition(record.Humidity, conditions.Humidity)
                    },
                    Light = new()
                    {
                        ExpectedValue = conditions.Light.ExpectedValue,
                        AllowedDeviation = conditions.Light.AllowedDeviation,
                        Actual = record.Light,
                        IsViolated = IsViolatedDecimalCondition(record.Light, conditions.Light)
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

    private static bool IsViolatedDecimalCondition(decimal actualVal, Condition<decimal> condition)
    {
        return condition.ExpectedValue - condition.AllowedDeviation > actualVal
            || actualVal > condition.ExpectedValue + condition.AllowedDeviation;
    }

    private static bool IsViolatedOrientationCondition(ConditionsRecord record, Condition<Orientation> condition)
    {
        var isViolatedX = IsViolatedDecimalCondition(record.Orientation.X, new()
        {
            ExpectedValue = condition.ExpectedValue.X,
            AllowedDeviation = condition.AllowedDeviation.X
        });

        var isViolatedY = IsViolatedDecimalCondition(record.Orientation.Y, new()
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

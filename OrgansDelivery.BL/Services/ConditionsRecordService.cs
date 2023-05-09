using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IConditionsRecordService
{
    Result<List<ConditionsRecordDto>> GetOrganRecords(Guid organId);
    List<ConditionsViolation> GetOrganViolations(Guid organId);
    Task<Result<ConditionsRecordDto>> AddConditionsRecordAsync(CreateConditionsRecordModel model);
}

public class ConditionsRecordService : IConditionsRecordService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public ConditionsRecordService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public Result<List<ConditionsRecordDto>> GetOrganRecords(Guid organId)
    {
        var deviceExists = _context.Devices.Any(c => c.Id == organId);
        if (!deviceExists)
        {
            return Result.Fail("Device not found");
        }

        var history = _context.Records
            .Where(c => c.Container.OrganId == organId)
            .ToList();

        var dtos = _mapper.Map<List<ConditionsRecordDto>>(history);

        return dtos;
    }

    public List<ConditionsViolation> GetOrganViolations(Guid organId)
    {
        var containerByIdMap = _context.Containers
            .Include(c => c.Conditions)
            .ToDictionary(c => c.Id);

        var history = _context.Records
            .Where(r => r.Container.OrganId == organId)
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

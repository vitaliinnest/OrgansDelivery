using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            .Where(c => c.OrganId == organId)
            .ToList();

        var dtos = _mapper.Map<List<ConditionsRecordDto>>(history);

        return dtos;
    }

    public List<ConditionsViolation> GetOrganViolations(Guid organId)
    {
        var records = _context.Records
            .Where(r => r.OrganId == organId)
            .ToList();

        var violations = records
            .Select(r =>
            {
                return new ConditionsViolation()
                {
                    RecordId = r.Id,
                    Temperature = new()
                    {
                        ExpectedValue = r.Conditions.Temperature.ExpectedValue,
                        AllowedDeviation = r.Conditions.Temperature.AllowedDeviation,
                        Actual = r.Temperature,
                        IsViolated = IsViolatedDecimalCondition(r.Temperature, r.Conditions.Temperature)
                    },
                    Humidity = new()
                    {
                        ExpectedValue = r.Conditions.Humidity.ExpectedValue,
                        AllowedDeviation = r.Conditions.Humidity.AllowedDeviation,
                        Actual = r.Humidity,
                        IsViolated = IsViolatedDecimalCondition(r.Humidity, r.Conditions.Humidity)
                    },
                    Light = new()
                    {
                        ExpectedValue = r.Conditions.Light.ExpectedValue,
                        AllowedDeviation = r.Conditions.Light.AllowedDeviation,
                        Actual = r.Light,
                        IsViolated = IsViolatedDecimalCondition(r.Light, r.Conditions.Light)
                    },
                    Orientation = new()
                    {
                        ExpectedValue = r.Conditions.Orientation.ExpectedValue,
                        AllowedDeviation = r.Conditions.Orientation.AllowedDeviation,
                        Actual = r.Orientation,
                        IsViolated = IsViolatedOrientationCondition(r, r.Conditions.Orientation)
                    }
                };
            })
            .Where(r => r.IsViolated())
            .ToList();

        return violations;
    }

	public async Task<Result<ConditionsRecordDto>> AddConditionsRecordAsync(CreateConditionsRecordModel model)
	{
		var device = _context.Devices
            .IgnoreQueryFilters()
            .Include(d => d.Container)
            .FirstOrDefault(d => d.Id == model.Device_id);

		if (device == null)
		{
			return Result.Fail("Device not found");
		}

  //      var organ = _context.Organs
  //          .IgnoreQueryFilters()
  //          .FirstOrDefault(o => o.Id == device.Container.OrganId);

		//var validationResult = await _genericValidator.ValidateAsync(model);
		//if (!validationResult.IsValid)
		//{
		//	return Result.Fail(validationResult.ToString());
		//}

		var record = _mapper.Map<ConditionsRecord>(model);
		//record.TenantId = device.TenantId;
  //      record.OrganId = organ.Id;
		//record.ConditionsId = organ.ConditionsId;

		//_context.Add(record);
		//_context.SaveChanges();

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

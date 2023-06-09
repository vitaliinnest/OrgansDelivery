﻿using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IConditionsRecordService
{
    Result<List<ConditionsRecordDto>> GetOrganRecords(Guid organId);
    Result AddConditionsRecord(CreateConditionsRecordModel model);
    List<ConditionsViolation> GetOrganViolations(Guid organId);
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
        var organExists = _context.Organs.Any(o => o.Id == organId);
        if (!organExists)
        {
            return Result.Fail("Organ not found");
        }

        var records = _context.Records
            .Include(r => r.Conditions)
            .Include(r => r.Organ)
            .Where(c => c.OrganId == organId)
            .ToList();

        return _mapper.Map<List<ConditionsRecordDto>>(records);
    }

	public Result AddConditionsRecord(CreateConditionsRecordModel model)
	{
		var device = _context.Devices
			.IgnoreQueryFilters()
			.Include(d => d.Container)
			.ThenInclude(c => c.Organ)
			.FirstOrDefault(d => d.Id == model.Device_id);

		if (device == null)
		{
			return Result.Fail("Device not found");
		}

		var organ = device.Container.Organ;

		var record = _mapper.Map<ConditionsRecord>(model);
        record.TenantId = organ.TenantId;
		record.OrganId = organ.Id;
		record.ConditionsId = organ.ConditionsId;

		_context.Add(record);
		_context.SaveChanges();

		return Result.Ok();
	}

	public List<ConditionsViolation> GetOrganViolations(Guid organId)
    {
        var records = _context.Records
            .Include(r => r.Conditions)
            .Where(r => r.OrganId == organId)
            .ToList();

        return records.Select(r => new ConditionsViolation()
		{
			Record = new()
            {
                Id = r.Id,
                ConditionsId = r.ConditionsId,
				OrganId = r.OrganId,
				DateTime = r.DateTime,
                Humidity = r.Humidity,
                Light = r.Light,
                Orientation = r.Orientation,
                Temperature = r.Temperature,
            },
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
		})
        .Where(r => r.IsViolated())
        .ToList();
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

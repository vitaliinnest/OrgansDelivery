using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IConditionsService
{
    List<ConditionsDto> GetConditions();
    Task<Result<ConditionsDto>> CreateConditionsAsync(
        ConditionsFormValues model);
    Task<Result<ConditionsDto>> UpdateConditionsAsync(
        Guid conditionsId, ConditionsFormValues model);
    Result DeleteConditions(Guid conditionId);
}

public class ConditionsService : IConditionsService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public ConditionsService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public List<ConditionsDto> GetConditions()
    {
		var conditions = _context.Conditions
			.Where(c => !c.IsArchival)
			.ToList();

        return _mapper.Map<List<ConditionsDto>>(conditions);
	}

	public async Task<Result<ConditionsDto>> CreateConditionsAsync(
        ConditionsFormValues model)
    {
        var validationResult = await ValidateConditionsFormValuesAsync(model);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }

		if (_context.Conditions.Any(p => p.Name.ToLower() == model.Name.ToLower()))
		{
			return Result.Fail("Conditions with given name already exist");
		}

		var conditions = _mapper.Map<Conditions>(model);

		_context.Add(conditions);
        _context.SaveChanges();

        return _mapper.Map<ConditionsDto>(conditions);
    }

    public async Task<Result<ConditionsDto>> UpdateConditionsAsync(
        Guid conditionsId, ConditionsFormValues model)
    {
		var validationResult = await ValidateConditionsFormValuesAsync(model);
		if (validationResult.IsFailed)
		{
			return validationResult;
		}

		if (_context.Conditions.Any(c => c.Id != conditionsId && !c.IsArchival && c.Name.ToLower() == model.Name.ToLower()))
		{
			return Result.Fail("Conditions with given name already exist");
		}

		var findResult = FindConditions(conditionsId);
		if (findResult.IsFailed)
        {
            return findResult.ToResult();
        }

		var conditions = findResult.Value;
		var conditionsChanged = model.ConditionsChanged(conditions);
		var currentConditions = _mapper.Map(model, conditions);
		
		if (conditionsChanged)
		{
			currentConditions.IsArchival = true;

			var newConditions = CreateConditionsCopy(currentConditions);
			_context.Add(newConditions);

			var organIds = currentConditions.Organs.Select(o => o.Id).ToList();
			var organs = _context.Organs.Where(o => organIds.Contains(o.Id)).ToList();
			foreach (var organ in organs)
			{
				organ.ConditionsId = newConditions.Id;
			}

			_context.SaveChanges();
			return _mapper.Map<ConditionsDto>(newConditions);
		}

		_context.SaveChanges();
		return _mapper.Map<ConditionsDto>(currentConditions);
	}

	public Result DeleteConditions(Guid conditionId)
    {
        var conditionPreset = _context.Conditions
            .FirstOrDefault(c => c.Id == conditionId);

        if (conditionPreset == null)
        {
            return Result.Fail("Condition preset not found");
        }

        var conditionUsed = _context.Organs.Any(c => c.ConditionsId == conditionId);
        if (conditionUsed)
        {
            return Result.Fail("Condition is used by a container(s). First remove it from it(them)");
        }

        _context.Remove(conditionPreset);
        _context.SaveChanges();

        return Result.Ok();
    }

    private async Task<Result> ValidateConditionsFormValuesAsync(ConditionsFormValues model)
    {
		var validationResult = await _genericValidator.ValidateAsync(model);
		if (!validationResult.IsValid)
		{
			return Result.Fail(validationResult.ToString());
		}

        return Result.Ok();
	}

    private Result<Conditions> FindConditions(Guid conditionsId)
    {
		var conditions = _context.Conditions
			.Include(c => c.Organs)
			.FirstOrDefault(c => c.Id == conditionsId && !c.IsArchival);

		if (conditions == null)
		{
			return Result.Fail("Conditions not found");
		}

        return conditions;
	}

	private static Conditions CreateConditionsCopy(Conditions conditions)
	{
		return new Conditions()
		{
			Name = conditions.Name,
			Description = conditions.Description,
			IsArchival = false,
			Humidity = new()
			{
				AllowedDeviation = conditions.Humidity.AllowedDeviation,
				ExpectedValue = conditions.Humidity.ExpectedValue,
			},
			Temperature = new()
			{
				AllowedDeviation = conditions.Temperature.AllowedDeviation,
				ExpectedValue = conditions.Temperature.ExpectedValue,
			},
			Light = new()
			{
				AllowedDeviation = conditions.Light.AllowedDeviation,
				ExpectedValue = conditions.Light.ExpectedValue,
			},
			Orientation = new()
			{
				AllowedDeviation = new()
				{
					X = conditions.Orientation.AllowedDeviation.X,
					Y = conditions.Orientation.AllowedDeviation.Y,
				},
				ExpectedValue = new()
				{
					X = conditions.Orientation.ExpectedValue.X,
					Y = conditions.Orientation.ExpectedValue.Y,
				}
			}
		};
	}
}

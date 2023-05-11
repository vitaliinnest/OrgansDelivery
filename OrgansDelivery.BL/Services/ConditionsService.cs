using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.BL.Services;

public interface IConditionsService
{
    Task<Result<Conditions>> CreateConditionsAsync(
        CreateConditionsModel model);
    Task<Result<Conditions>> UpdateConditionsAsync(
        Guid conditionsId, UpdateConditionsModel model);
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

    public async Task<Result<Conditions>> CreateConditionsAsync(
        CreateConditionsModel model)
    {
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var conditions = _mapper.Map<Conditions>(model);
        if (_context.Conditions.Any(p => p.Name.ToLower() == conditions.Name.ToLower()))
        {
            return Result.Fail("Conditions with given name already exist");
        }

        _context.Add(conditions);
        _context.SaveChanges();

        return conditions;
    }

    public async Task<Result<Conditions>> UpdateConditionsAsync(
        Guid conditionsId, UpdateConditionsModel model)
    {
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

		var conditions = _context.Conditions
			.Include(c => c.Organs)
			.FirstOrDefault(c => c.Id == conditionsId && !c.IsArchival);

		if (conditions == null)
		{
			return Result.Fail("Conditions not found");
		}

		var updatedConditions = _mapper.Map(model, conditions);
		
        if (model.AreConditionsUpdated())
		{
            var newConditions = _context.DetachedClone(updatedConditions);
            newConditions.Id = Guid.Empty;
            _context.Add(newConditions);

			updatedConditions.IsArchival = true;
		}

		_context.Update(updatedConditions);

		foreach (var organ in conditions.Organs)
        {
            organ.ConditionsId = conditionsId;
        }

		_context.SaveChanges();

		return updatedConditions;
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
}

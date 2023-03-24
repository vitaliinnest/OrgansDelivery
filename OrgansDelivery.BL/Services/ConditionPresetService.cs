using AutoMapper;
using FluentResults;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IConditionPresetService
{
    Task<Result<Conditions>> CreateContainerConditionsAsync(
        CreateContainerConditionsModel model);
    Result DeleteContainerConditions(Guid conditionId);
}

public class ConditionPresetService : IConditionPresetService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public ConditionPresetService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public async Task<Result<Conditions>> CreateContainerConditionsAsync(CreateContainerConditionsModel model)
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

    // todo: update condition preset

    public Result DeleteContainerConditions(Guid conditionId)
    {
        var conditionPreset = _context.Conditions
            .FirstOrDefault(c => c.Id == conditionId);
        if (conditionPreset == null)
        {
            return Result.Fail("Condition preset not found");
        }
        
        _context.Remove(conditionPreset);
        _context.SaveChanges();

        return Result.Ok();
    }
}

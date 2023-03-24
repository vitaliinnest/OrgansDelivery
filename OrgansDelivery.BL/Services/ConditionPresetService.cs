using AutoMapper;
using FluentResults;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IConditionPresetService
{
    Task<Result<Conditions>> CreateContainerConditionsAsync(CreateContainerConditionsModel model);
    Result DeleteContainerConditions(Guid conditionPresetId);
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

        var preset = _mapper.Map<Conditions>(model);
        if (_context.Conditions.Any(p => p.Name.ToLower() == preset.Name.ToLower()))
        {
            return Result.Fail("Condition preset with given name already exists");
        }

        _context.Add(preset);
        _context.SaveChanges();

        return preset;
    }

    // todo: update condition preset

    public Result DeleteContainerConditions(Guid conditionPresetId)
    {
        var conditionPreset = _context.Conditions
            .FirstOrDefault(c => c.Id == conditionPresetId);
        if (conditionPreset == null)
        {
            return Result.Fail("Condition preset not found");
        }
        
        _context.Remove(conditionPreset);
        _context.SaveChanges();

        return Result.Ok();
    }
}

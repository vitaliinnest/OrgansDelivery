using AutoMapper;
using FluentResults;
using Mallytics.BL.Services;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IConditionPresetService
{
    Task<Result<ConditionPreset>> CreateConditionPresetAsync(CreateConditionsPresetModel model);
    Result DeleteConditionPreset(Guid conditionPresetId);
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

    public async Task<Result<ConditionPreset>> CreateConditionPresetAsync(CreateConditionsPresetModel model)
    {
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }
        
        var preset = _mapper.Map<ConditionPreset>(model);
        if (_context.ConditionPresets.Any(p => p.Name.ToLower() == preset.Name.ToLower()))
        {
            return Result.Fail("Condition preset with given name already exists");
        }
        
        _context.Add(preset);
        _context.SaveChanges();
        
        return preset;
    }

    // todo: update condition preset

    public Result DeleteConditionPreset(Guid conditionPresetId)
    {
        var conditionPreset = _context.ConditionPresets.FirstOrDefault(c => c.Id == conditionPresetId);
        if (conditionPreset == null)
        {
            return Result.Fail("Condition preset not found");
        }
        _context.Remove(conditionPreset);
        _context.SaveChanges();
        return Result.Ok();
    }
}

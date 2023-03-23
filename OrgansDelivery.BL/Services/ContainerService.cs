using AutoMapper;
using FluentResults;
using Mallytics.BL.Services;
using OrgansDelivery.BL.Extensions;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IContainerService
{
    Task<Result<Container>> AddOrganToContainerAsync(Guid containerId, Guid organId);
    Task<Result<Container>> CreateContainerAsync(CreateContainerModel model);
    Result DeleteContainer(Guid containerId);
}

public class ContainerService : IContainerService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public ContainerService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public async Task<Result<Container>> CreateContainerAsync(CreateContainerModel model)
    {
        // todo: add validator
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var conditionResult = GetContainerCondition(model);
        if (conditionResult.IsFailed)
        {
            return Result.Fail(conditionResult.ErrorMessagesToString());
        }

        var container = _mapper.Map<Container>(model);
        container.Conditions = conditionResult.Value;
        _context.Add(container);
        _context.SaveChanges();
        
        return container;
    }

    public Result DeleteContainer(Guid containerId)
    {
        var container = _context.Containers.FirstOrDefault(i => i.Id == containerId);
        if (container == null)
        {
            return Result.Fail("Organ not found");
        }

        if (container.OrganId.HasValue)
        {
            return Result.Fail("Organ is in container. Get it out of the container first");
        }

        _context.Remove(container);
        _context.SaveChanges();

        return Result.Ok();
    }

    private Result<Conditions> GetContainerCondition(CreateContainerModel model)
    {
        if (model.ConditionPresetId.HasValue)
        {
            var condition = _context.ConditionPresets
                .FirstOrDefault(p => p.Id == model.ConditionPresetId.Value);

            return Result.OkIf(condition != null, "ConditionPreset not found");
        }

        return Result.OkIf(model.Conditions != null, "Condition is not set");
    }
}

using AutoMapper;
using FluentResults;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IContainerService
{
    Task<Result<Container>> CreateContainerAsync(CreateContainerModel model);
    Result<Container> UpdateContainer(Guid containerId, UpdateContainerModel model);
    Result DeleteContainer(Guid containerId);
    Result<Container> AddOrganToContainerAsync(Guid containerId, Guid organId);
    Result<Container> RemoveOrganFromContainer(Guid containerId);
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
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var conditionsExist = _context.Conditions
            .Any(p => p.Id == model.ConditionsId);

        if (!conditionsExist)
        {
            return Result.Fail("Condition not found");
        }

        var container = _mapper.Map<Container>(model);
        _context.Add(container);
        _context.SaveChanges();

        return container;
    }

    public Result<Container> UpdateContainer(Guid containerId, UpdateContainerModel model)
    {
        // imagine validation is here
        
        var conditionsExist = _context.Conditions
            .Any(p => p.Id == model.ConditionsId);

        if (!conditionsExist)
        {
            return Result.Fail("Condition not found");
        }

        var container = _context.Containers.FirstOrDefault(o => o.Id == containerId);
        if (container == null)
        {
            return Result.Fail("Container not found");
        }

        var updated = _mapper.Map(model, container);

        _context.Update(updated);
        _context.SaveChanges();

        return updated;
    }

    public Result DeleteContainer(Guid containerId)
    {
        var container = _context.Containers.FirstOrDefault(i => i.Id == containerId);
        if (container == null)
        {
            return Result.Fail("Container not found");
        }

        if (container.OrganId.HasValue)
        {
            return Result.Fail("Organ is in container. Get it out of the container first");
        }

        _context.Remove(container);
        _context.SaveChanges();

        return Result.Ok();
    }

    public Result<Container> AddOrganToContainerAsync(Guid containerId, Guid organId)
    {
        var container = _context.Containers.FirstOrDefault(c => c.Id == containerId);
        if (container == null)
        {
            return Result.Fail("Container not found");
        }

        var organExists = _context.Organs.Any(o => o.Id == organId);
        if (!organExists)
        {
            return Result.Fail("Organ not found");
        }

        container.OrganId = organId;
        _context.SaveChanges();

        return container;
    }

    public Result<Container> RemoveOrganFromContainer(Guid containerId)
    {
        var container = _context.Containers.FirstOrDefault(c => c.Id == containerId);
        if (container == null)
        {
            return Result.Fail("Container not found");
        }

        if (!container.OrganId.HasValue)
        {
            return Result.Fail("Container contains no organ");
        }

        container.OrganId = null;
        _context.SaveChanges();

        return container;
    }

    //private Result<ExpectedConditions> BuildContainerConditions(CreateContainerModel model)
    //{
    //    if (model.ConditionPresetId.HasValue)
    //    {
    //        var condition = _context.Conditions
    //            .FirstOrDefault(p => p.Id == model.ConditionPresetId.Value);

    //        return Result.OkIf(condition != null, "Condition Preset not found");
    //    }

    //    return Result.OkIf(model.Conditions != null, "Condition is not set");
    //}
}

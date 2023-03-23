﻿using AutoMapper;
using FluentResults;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.BL.Services;

public interface IContainerService
{
    Task<Result<Container>> CreateContainerAsync(CreateContainerModel model);
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
        // todo: add validator
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var conditionsResult = BuildContainerConditions(model);
        if (conditionsResult.IsFailed)
        {
            return Result.Fail(conditionsResult.ErrorMessagesToString());
        }

        var container = _mapper.Map<Container>(model);
        container.Conditions = conditionsResult.Value;
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

    private Result<ExpectedConditions> BuildContainerConditions(CreateContainerModel model)
    {
        if (model.ConditionPresetId.HasValue)
        {
            var condition = _context.ConditionPresets
                .FirstOrDefault(p => p.Id == model.ConditionPresetId.Value);

            return Result.OkIf(condition != null, "Condition Preset not found");
        }

        return Result.OkIf(model.Conditions != null, "Condition is not set");
    }
}

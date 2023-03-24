﻿using AutoMapper;
using FluentResults;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IOrganService
{
    Task<Result<Organ>> CreateOrganAsync(CreateOrganModel model);
    Result DeleteOrgan(Guid organId);
}

public class OrganService : IOrganService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public OrganService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public async Task<Result<Organ>> CreateOrganAsync(CreateOrganModel model)
    {
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var organ = _mapper.Map<Organ>(model);
        if (_context.Organs.Any(p => p.Name.ToLower() == organ.Name.ToLower()))
        {
            return Result.Fail("Organ with given name already exists");
        }

        _context.Add(organ);
        _context.SaveChanges();

        return organ;
    }

    public Result DeleteOrgan(Guid organId)
    {
        var organ = _context.Organs.FirstOrDefault(i => i.Id == organId);
        if (organ == null)
        {
            return Result.Fail("Organ not found");
        }

        if (organ.ContainerId.HasValue)
        {
            return Result.Fail("Organ is in container. Get it out of the container first");
        }

        _context.Remove(organ);
        _context.SaveChanges();

        return Result.Ok();
    }
}

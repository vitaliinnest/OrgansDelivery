﻿using AutoMapper;
using FluentResults;
using Mallytics.BL.Services;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IOrganService
{
    Task<Result<Organ>> CreateOrganAsync(CreateOrganModel model);
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
        // todo: add validation
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
}

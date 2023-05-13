﻿using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Models;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;

namespace OrganStorage.BL.Services;

public interface ITenantService
{
    Task<Result<Tenant>> CreateTenantAsync(TenantFormValues model);
    Result<Tenant> UpdateTenant(TenantFormValues model);
    Task AddUserToTenantAsync(User user, Tenant tenant);
}

public class TenantService : ITenantService
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    private readonly IEnvironmentProvider _environmentProvider;
    private readonly IGenericValidator _genericValidator;

    public TenantService(
        UserManager<User> userManager,
        IServiceProvider serviceProvider,
        IMapper mapper,
        AppDbContext context,
        IEnvironmentProvider environmentProvider,
        IGenericValidator genericValidator)
    {
        _userManager = userManager;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
        _context = context;
        _environmentProvider = environmentProvider;
        _genericValidator = genericValidator;
    }

    public async Task<Result<Tenant>> CreateTenantAsync(TenantFormValues model)
    {
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        if (_environmentProvider.Tenant != null)
        {
            return Result.Fail("Tenant already exists");
        }

        var tenant = _mapper.Map<Tenant>(model);

        // todo: check if tenant.Id is set
        _context.Tenants.Add(tenant);
        _context.SaveChanges();

        EnvironmentSetter.SetTenant(tenant, _serviceProvider);

        await AddUserToTenantAsync(_environmentProvider.User, tenant);

        return tenant;
    }

    public Result<Tenant> UpdateTenant(TenantFormValues model)
    {
        // imagine validation here

        var tenant = _environmentProvider.Tenant;
        if (tenant == null)
        {
            return Result.Fail("Tenant not set");
        }

        var updated = _mapper.Map(model, tenant);

        _context.Update(updated);
        _context.SaveChanges();

        return updated;
    }

    public async Task AddUserToTenantAsync(User user, Tenant tenant)
    {
        user.TenantId = tenant.Id;
        await _userManager.UpdateAsync(user);
        EnvironmentSetter.SetUser(user, _serviceProvider);
    }
}

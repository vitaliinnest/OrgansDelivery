using AutoMapper;
using FluentResults;
using Mallytics.BL.Services;
using Microsoft.AspNetCore.Identity;
using OrgansDelivery.BL.Models;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.DAL.Services;

namespace OrgansDelivery.BL.Services;

public interface ITenantService
{
    Task<Result<Tenant>> CreateTenantAsync(CreateTenantModel model);
    Task AddUserToTenantAsync(User user, Tenant tenant);
}

public class TenantService : ITenantService
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;
    private readonly IEnvironmentProvider _environmentProvider;
    private readonly IGenericValidator _genericValidator;

    public TenantService(
        UserManager<User> userManager,
        IServiceProvider serviceProvider,
        IMapper mapper,
        AppDbContext appDbContext,
        IEnvironmentProvider environmentProvider,
        IGenericValidator genericValidator
        )
    {
        _userManager = userManager;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
        _appDbContext = appDbContext;
        _environmentProvider = environmentProvider;
        _genericValidator = genericValidator;
    }

    public async Task<Result<Tenant>> CreateTenantAsync(CreateTenantModel model)
    {
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var tenant = _mapper.Map<Tenant>(model);
        
        // todo: check if tenant.Id is set
        _appDbContext.Tenants.Add(tenant);
        _appDbContext.SaveChanges();

        EnvironmentSetter.SetTenant(tenant, _serviceProvider);

        await AddUserToTenantAsync(_environmentProvider.User, tenant);

        return tenant;
    }
    
    public async Task AddUserToTenantAsync(User user, Tenant tenant)
    {
        user.TenantId = tenant.Id;
        await _userManager.UpdateAsync(user);
        EnvironmentSetter.SetUser(user, _serviceProvider);
    }
}

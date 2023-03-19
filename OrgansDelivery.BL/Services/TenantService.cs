using Microsoft.AspNetCore.Identity;
using OrgansDelivery.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgansDelivery.BL.Services;

public interface ITenantService
{
    Task<Tenant> CreateTenantAsync(CreateTenantModel model);
    Task AddUserToTenantAsync(User user, Tenant tenant);
}

public class TenantService : ITenantService
{
    private readonly UserManager<User> _userManager;

    public TenantService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public Task<Tenant> CreateTenantAsync(CreateTenantModel model)
    {
        throw new NotImplementedException();
    }
    
    public async Task AddUserToTenantAsync(User user, Tenant tenant)
    {
        user.TenantId = tenant.Id;
        await _userManager.UpdateAsync(user);
    }
}

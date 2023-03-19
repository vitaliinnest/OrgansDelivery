using Azure.Core;
using Microsoft.AspNetCore.Identity;
using OrgansDelivery.BL.Consts;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IRolesService
{
    Task InitializeUserRoleAsync(User user, RegisterRequest registerRequest);
}

public class RolesService : IRolesService
{
    private readonly UserManager<User> _userManager;

    public RolesService(
        UserManager<User> userManager
        )
    {
        _userManager = userManager;
    }

    public async Task InitializeUserRoleAsync(User user, RegisterRequest registerRequest)
    {
        var role = !registerRequest.InviteCode.HasValue
            ? UserRoles.MANAGER
            : UserRoles.EMPLOYEE;

        await _userManager.AddToRoleAsync(user, role);
    }
}

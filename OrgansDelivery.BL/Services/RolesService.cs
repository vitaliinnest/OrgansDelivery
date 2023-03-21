﻿using Microsoft.AspNetCore.Identity;
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
    private readonly IInviteService _inviteService;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RolesService(
        UserManager<User> userManager,
        IInviteService inviteService,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _inviteService = inviteService;
        _roleManager = roleManager;
    }

    public async Task InitializeUserRoleAsync(User user, RegisterRequest registerRequest)
    {
        // todo: add roles endpoint
        var invite = _inviteService.GetRegisterInvite(registerRequest);
        if (invite == null)
        {
            return;
        }
        var role = await _roleManager.FindByIdAsync(invite.RoleId.ToString());
        if (role == null)
        {
            return;
        }
        await _userManager.AddToRoleAsync(user, role.Name);
    }
}

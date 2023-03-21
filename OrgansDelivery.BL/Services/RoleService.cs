using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OrgansDelivery.BL.Models;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IRoleService
{
    List<RoleDto> GetAllRoles();
    Task<IdentityRole<Guid>> GetUserRoleAsync(Guid userId);
    Task<IdentityRole<Guid>> GetUserRoleAsync(User user);
    Task InitializeUserRoleIfInvitedAsync(User user, RegisterRequest registerRequest);
}

public class RoleService : IRoleService
{
    private readonly UserManager<User> _userManager;
    private readonly IInviteService _inviteService;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IMapper _mapper;

    public RoleService(
        UserManager<User> userManager,
        IInviteService inviteService,
        RoleManager<IdentityRole<Guid>> roleManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _inviteService = inviteService;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task InitializeUserRoleIfInvitedAsync(User user, RegisterRequest registerRequest)
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

    public async Task<IdentityRole<Guid>> GetUserRoleAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return null;
        }
        return await GetUserRoleAsync(user);
    }

    public async Task<IdentityRole<Guid>> GetUserRoleAsync(User user)
    {
        var roleName = (await _userManager.GetRolesAsync(user)).SingleOrDefault();
        if (roleName == null)
        {
            return null;
        }
        return await _roleManager.FindByNameAsync(roleName);
    }

    public List<RoleDto> GetAllRoles()
    {
        var identityRoles = _roleManager.Roles.ToList();
        return _mapper.Map<List<RoleDto>>(identityRoles);
    }
}

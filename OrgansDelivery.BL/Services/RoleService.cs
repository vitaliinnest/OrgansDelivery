using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Models.Auth;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IRoleService
{
    List<RoleDto> GetRoles();
    Task<IdentityRole<Guid>> GetUserRoleAsync(Guid userId);
    Task<IdentityRole<Guid>> GetUserRoleAsync(User user);
    Task InitializeUserRoleAsync(User user, RegisterRequest registerRequest);
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

    public async Task InitializeUserRoleAsync(User user, RegisterRequest registerRequest)
    {
        var roleName = await CalculateUserRoleAsync(registerRequest);
        if (roleName == null)
        {
            return;
        }

        await _userManager.AddToRoleAsync(user, roleName);
    }

    private async Task<string> CalculateUserRoleAsync(RegisterRequest registerRequest)
    {
        var invite = _inviteService.GetRegisterInvite(registerRequest);
        if (invite == null)
        {
            return UserRoles.MANAGER;
        }

        var role = await _roleManager.FindByIdAsync(invite.RoleId.ToString());
        return role?.Name;
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

    public List<RoleDto> GetRoles()
    {
        var identityRoles = _roleManager.Roles.Where(r => r.Name != UserRoles.ADMIN).ToList();
        return _mapper.Map<List<RoleDto>>(identityRoles);
    }
}

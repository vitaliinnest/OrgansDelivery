using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Entities.Auth;

namespace OrganStorage.BL.Services;

public interface IRoleService
{
    Task<IdentityRole<Guid>> GetUserRoleAsync(Guid userId);
    Task<IdentityRole<Guid>> GetUserRoleAsync(User user);
    Task AddUserToRoleAsync(User user, RegisterRequest registerRequest);
}

public class RoleService : IRoleService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IMapper _mapper;

    public RoleService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task AddUserToRoleAsync(User user, RegisterRequest registerRequest)
    {
        await _userManager.AddToRoleAsync(user, UserRoles.USER);
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
}

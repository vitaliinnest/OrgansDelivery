using Microsoft.AspNetCore.Identity;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;
using System.Security.Claims;

namespace OrgansDelivery.BL.Services;

public interface IClaimsService
{
    Task<List<Claim>> GetClaimsForAuthUserAsync(Guid userId);
}

public class ClaimsService : IClaimsService
{
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<User> _userManager;
    private readonly IRoleService _roleService;

    public ClaimsService(
        AppDbContext appDbContext,
        UserManager<User> userManager,
        IRoleService roleService
        )
    {
        _appDbContext = appDbContext;
        _userManager = userManager;
        _roleService = roleService;
    }

    public async Task<List<Claim>> GetClaimsForAuthUserAsync(Guid userId)
    {
        var role = await _roleService.GetUserRoleAsync(userId);
        return new()
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Role, role?.Name ?? string.Empty),
        };
    }
}

using OrganStorage.DAL.Entities;
using System.Security.Claims;

namespace OrganStorage.BL.Services;

public interface IClaimsService
{
    Task<List<Claim>> GetClaimsForAuthUserAsync(User user, Tenant tenant);
}

public class ClaimsService : IClaimsService
{
    private readonly IRoleService _roleService;

    public ClaimsService(
        IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<List<Claim>> GetClaimsForAuthUserAsync(User user, Tenant tenant)
    {
        var role = await _roleService.GetUserRoleAsync(user.Id);
        return new()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, role?.Name ?? string.Empty),
            new("tenantId", tenant?.Id.ToString()),
        };
    }
}

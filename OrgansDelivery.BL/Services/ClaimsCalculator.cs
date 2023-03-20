using Microsoft.AspNetCore.Identity;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;
using System.Security.Claims;

namespace OrgansDelivery.BL.Services;

public interface IClaimsCalculator
{
    Task<List<Claim>> GetClaimsForAuthUserAsync(Guid userId);
}

public class ClaimsCalculator : IClaimsCalculator
{
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<User> _userManager;

    public ClaimsCalculator(
        AppDbContext appDbContext,
        UserManager<User> userManager
        )
    {
        _appDbContext = appDbContext;
        _userManager = userManager;
    }

    public async Task<List<Claim>> GetClaimsForAuthUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var role = (await _userManager.GetRolesAsync(user)).Single();
        return new()
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Role, role)
        };
    }
}

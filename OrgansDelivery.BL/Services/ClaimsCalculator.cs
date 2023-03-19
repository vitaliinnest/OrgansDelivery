using OrgansDelivery.DAL.Data;
using System.Security.Claims;

namespace OrgansDelivery.BL.Services;

internal interface IClaimsCalculator
{
    Task<List<Claim>> GetClaimsForAuthUserAsync(Guid userId);
}

public class ClaimsCalculator : IClaimsCalculator
{
    private readonly AppDbContext _appDbContext;

    public ClaimsCalculator(
        AppDbContext appDbContext
        )
    {
        _appDbContext = appDbContext;
    }

    public Task<List<Claim>> GetClaimsForAuthUserAsync(Guid userId)
    {
        //var role = (await _userManager.GetRolesAsync(user)).Single();
        return Task.FromResult(new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            //new(ClaimTypes.Role, role)
        });
    }
}

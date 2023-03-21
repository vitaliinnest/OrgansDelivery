using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Identity;
using OrgansDelivery.DAL.Entities;
using System.Security.Claims;

namespace OrgansDelivery.Web.Common.Services;

public interface IUserRequestResolver
{
    Task<User> ResolveUserAsync(ClaimsPrincipal user);
}

public class UserRequestResolver : IUserRequestResolver
{
    private readonly UserManager<User> _userManager;

    public UserRequestResolver(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> ResolveUserAsync(ClaimsPrincipal user)
    {
        var identity = user.Identity as ClaimsIdentity;
        var userClaims = identity.Claims;
        var id = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return null;
        }
        return await _userManager.FindByIdAsync(userId);
    }
}

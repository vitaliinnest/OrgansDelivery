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
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return null;
        }
        // "164f1a2d-3144-459b-dfa8-08db2a0e07b9"
        return await _userManager.FindByIdAsync(userId);
    }
}

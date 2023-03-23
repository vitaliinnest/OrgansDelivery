using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;

namespace OrganStorage.Web.Common.Services;

public interface IUserRequestResolver
{
    User ResolveUser(ClaimsPrincipal user);
}

public class UserRequestResolver : IUserRequestResolver
{
    private readonly UserManager<User> _userManager;

    public UserRequestResolver(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public User ResolveUser(ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return null;
        }
        // "164f1a2d-3144-459b-dfa8-08db2a0e07b9"
        return _userManager.FindByIdIgnoreQueryFilters(Guid.Parse(userId));
    }
}

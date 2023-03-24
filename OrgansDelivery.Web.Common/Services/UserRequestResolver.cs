using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;

namespace OrganStorage.Web.Common.Services;

public interface IUserRequestResolver
{
    User ResolveUser(Guid userId);
}

public class UserRequestResolver : IUserRequestResolver
{
    private readonly UserManager<User> _userManager;

    public UserRequestResolver(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public User ResolveUser(Guid userId)
    {
        return _userManager.FindByIdIgnoreQueryFilters(userId);
    }
}

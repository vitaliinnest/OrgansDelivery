using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Extensions;

public static class UserManagerExtensions
{
    public static User FindByEmailIgnoreQueryFilters(this UserManager<User> userManager, string email)
    {
        return userManager.Users
            .IgnoreQueryFilters()
            .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
    }
}

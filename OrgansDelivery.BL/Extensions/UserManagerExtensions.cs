﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Extensions;

public static class UserManagerExtensions
{
    public static User FindByEmailIgnoreQueryFilters(this UserManager<User> userManager, string email)
    {
        return userManager.Users
            .IgnoreQueryFilters()
            .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
    }

    public static User FindByIdIgnoreQueryFilters(this UserManager<User> userManager, Guid userId)
    {
        return userManager.Users
            .IgnoreQueryFilters()
            .FirstOrDefault(u => u.Id == userId);
    }
}

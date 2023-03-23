using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Services;
using OrganStorage.DAL.Entities;
using System.Text;

namespace OrganStorage.BL.Extensions;

public static class IRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string> PasswordAsync<T>(
        this IRuleBuilder<T, string> ruleBuilder, UserManager<User> userManager)
    {
        var errMessage = new StringBuilder();
        return ruleBuilder.MustAsync(async (password, _) =>
        {
            foreach (var validator in userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(userManager, user: new(), password);
                if (!result.Succeeded)
                {
                    errMessage.AppendLine(result.ToString());
                }
            }
            return errMessage.Length == 0;
        }).WithMessage(_ => errMessage.ToString());
    }

    public static IRuleBuilderOptions<T, Guid> RoleId<T>(
       this IRuleBuilder<T, Guid> ruleBuilder, IRoleService roleService)
    {
        return ruleBuilder.Must(roleId => roleService.GetRoles().Any(r => r.Id == roleId));
    }

    public static IRuleBuilderOptions<T, decimal> Temperature<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.InclusiveBetween(-100m, 100m);
    }

    public static IRuleBuilderOptions<T, decimal> Humidity<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.InclusiveBetween(0m, 100m);
    }

    public static IRuleBuilderOptions<T, decimal> Light<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.InclusiveBetween(0m, 20000m);
    }

    public static IRuleBuilderOptions<T, decimal> OrientationAxis<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.InclusiveBetween(-90m, 90m);
    }
}

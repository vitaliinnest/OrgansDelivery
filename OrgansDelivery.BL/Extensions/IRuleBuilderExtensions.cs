using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrgansDelivery.BL.Services;
using OrgansDelivery.DAL.Entities;
using System.Text;

namespace OrgansDelivery.BL.Extensions;

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
}

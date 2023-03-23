using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Consts;
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

    public static IRuleBuilderOptions<T, Condition<decimal>> TemperatureCondition<T>(
        this IRuleBuilder<T, Condition<decimal>> ruleBuilder)
    {
        return ruleBuilder.ApplyConditionRules(ConditionConsts.Temperature.MIN, ConditionConsts.Temperature.MAX);
    }

    public static IRuleBuilderOptions<T, decimal> Temperature<T>(
        this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.InclusiveBetween(ConditionConsts.Temperature.MIN, ConditionConsts.Temperature.MAX);
    }

    public static IRuleBuilderOptions<T, Condition<decimal>> HumidityCondition<T>(
        this IRuleBuilder<T, Condition<decimal>> ruleBuilder)
    {
        return ruleBuilder.ApplyConditionRules(ConditionConsts.Humidity.MIN, ConditionConsts.Humidity.MAX);
    }

    public static IRuleBuilderOptions<T, decimal> Humidity<T>(
        this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.InclusiveBetween(ConditionConsts.Humidity.MIN, ConditionConsts.Humidity.MAX);
    }

    public static IRuleBuilderOptions<T, Condition<decimal>> LightCondition<T>(
        this IRuleBuilder<T, Condition<decimal>> ruleBuilder)
    {
        return ruleBuilder.ApplyConditionRules(ConditionConsts.Light.MIN, ConditionConsts.Light.MAX);
    }

    public static IRuleBuilderOptions<T, decimal> Light<T>(
        this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.InclusiveBetween(ConditionConsts.Light.MIN, ConditionConsts.Light.MAX);
    }

    public static IRuleBuilderOptions<T, Condition<Orientation>> OrientationAxisCondition<T>(
        this IRuleBuilder<T, Condition<Orientation>> ruleBuilder)
    {
        return ruleBuilder.ChildRules(condition =>
        {
            condition
                .RuleFor(c => c.ExpectedValue)
                .ChildRules(v =>
                {
                    v.RuleFor(t => t.X)
                        .OrientationAxis();

                    v.RuleFor(t => t.Y)
                        .OrientationAxis();
                });

            condition
                .RuleFor(c => c.AllowedDiviation)
                .ChildRules(v =>
                {
                    v.RuleFor(t => t.X)
                        .OrientationAxis();

                    v.RuleFor(t => t.Y)
                        .OrientationAxis();
                });
        });
    }

    public static IRuleBuilderOptions<T, decimal> OrientationAxis<T>(
        this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder
            .InclusiveBetween(ConditionConsts.OrientationAxis.MIN, ConditionConsts.OrientationAxis.MAX);
    }

    private static IRuleBuilderOptions<T, Condition<decimal>> ApplyConditionRules<T>(
        this IRuleBuilder<T, Condition<decimal>> ruleBuilder, decimal min, decimal max)
    {
        return ruleBuilder.ChildRules(condition =>
        {
            condition
                .RuleFor(c => c.ExpectedValue)
                .InclusiveBetween(min, max);

            condition
                .RuleFor(c => c.AllowedDiviation)
                .InclusiveBetween(min, max);
        });
    }
}

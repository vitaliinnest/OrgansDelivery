using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Consts;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Validators;

public class CreateContainerModelValidator : AbstractValidator<CreateContainerModel>
{
    public CreateContainerModelValidator(UserManager<User> userManager)
    {
        RuleFor(c => c.ConditionsIntervalCheckInSecs)
            .InclusiveBetween(
                ConditionConsts.ConditionsIntervalCheckInSecs.MIN,
                ConditionConsts.ConditionsIntervalCheckInSecs.MAX);
    }
}

using FluentValidation;
using OrganStorage.BL.Consts;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Validators;

public class CreateContainerValidator : AbstractValidator<CreateContainerModel>
{
    public CreateContainerValidator()
    {
        RuleFor(c => c.ConditionsIntervalCheckInSecs)
            .InclusiveBetween(
                ConditionConsts.ConditionsIntervalCheckInSecs.MIN,
                ConditionConsts.ConditionsIntervalCheckInSecs.MAX);
    }
}

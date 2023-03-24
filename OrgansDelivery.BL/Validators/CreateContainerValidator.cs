using FluentValidation;
using OrganStorage.BL.Consts;
using OrganStorage.DAL.Entities;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class CreateContainerValidator : AbstractValidator<CreateContainerModel>
{
    public CreateContainerValidator()
    {
        RuleFor(t => t.Name)
            .NotNull()
            .NotEmpty()
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH);

        RuleFor(t => t.Description)
            .MaximumLength(GeneralConsts.MAX_LENGTH);

        RuleFor(c => c.ConditionsIntervalCheckInSecs)
            .InclusiveBetween(
                ConditionConsts.ConditionsIntervalCheckInSecs.MIN,
                ConditionConsts.ConditionsIntervalCheckInSecs.MAX);
    }
}

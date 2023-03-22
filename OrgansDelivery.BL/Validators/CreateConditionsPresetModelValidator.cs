using FluentValidation;
using OrgansDelivery.DAL.Entities;
using static OrgansDelivery.BL.Consts.ValidatorConsts;

namespace OrgansDelivery.BL.Validators;

public class CreateConditionsPresetModelValidator : AbstractValidator<CreateConditionsPresetModel>
{
    public CreateConditionsPresetModelValidator()
    {
        Include(new ConditionsValidator());

        RuleFor(t => t.Name)
            .NotNull()
            .NotEmpty()
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH);

        RuleFor(t => t.Description)
            .MaximumLength(GeneralConsts.MAX_LENGTH);
    }
}

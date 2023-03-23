using FluentValidation;
using OrganStorage.BL.Models;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class CreateTenantModelValidator : AbstractValidator<CreateTenantModel>
{
    public CreateTenantModelValidator()
    {
        RuleFor(t => t.Url)
            .NotNull()
            .NotEmpty()
            .Matches(GeneralConsts.STR_INT_DASH_REGEX)
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH);

        RuleFor(t => t.Name)
            .NotNull()
            .NotEmpty()
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH);

        RuleFor(t => t.Description)
            .MaximumLength(GeneralConsts.MAX_LENGTH);
    }
}

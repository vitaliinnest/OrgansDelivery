using FluentValidation;
using OrganStorage.BL.Models;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class TenantFormValuesValidator : AbstractValidator<TenantFormValues>
{
    public TenantFormValuesValidator()
    {
        RuleFor(t => t.Name)
            .NotNull()
            .NotEmpty()
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH);
    }
}

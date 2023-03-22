using FluentValidation;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Validators;

public class ConditionsValidator : AbstractValidator<Conditions>
{
    public ConditionsValidator()
    {
        RuleFor(p => p.Temperature)
            .InclusiveBetween(-100m, 100m);

        RuleFor(p => p.Temperature)
            .InclusiveBetween(0m, 100m);

        RuleFor(p => p.Light)
            .InclusiveBetween(0m, 20000m);

        RuleFor(p => p.OrientationLimits)
            .SetValidator(new OrientationLimitsValidator());
    }
}

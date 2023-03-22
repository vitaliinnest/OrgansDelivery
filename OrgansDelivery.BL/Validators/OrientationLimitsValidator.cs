using FluentValidation;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Validators;

public class OrientationLimitsValidator : AbstractValidator<OrientationLimits>
{
    public OrientationLimitsValidator()
    {
        RuleFor(l => l.XLimit)
            .InclusiveBetween(-90m, 90m);

        RuleFor(l => l.YLimit)
            .InclusiveBetween(-90m, 90m);
    }
}

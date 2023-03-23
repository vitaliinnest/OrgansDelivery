using FluentValidation;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Validators;

public class OrientationValidator : AbstractValidator<Orientation>
{
    public OrientationValidator()
    {
        RuleFor(o => o.X)
            .OrientationAxis();

        RuleFor(o => o.Y)
            .OrientationAxis();
    }
}

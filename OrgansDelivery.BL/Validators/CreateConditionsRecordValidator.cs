using FluentValidation;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Validators;

public class CreateConditionsRecordValidator : AbstractValidator<CreateConditionsRecordModel>
{
    public CreateConditionsRecordValidator()
    {
        RuleFor(p => p.Temperature)
            .Temperature();

        RuleFor(p => p.Humidity)
            .Humidity();

        RuleFor(p => p.Light)
            .Light();

        RuleFor(p => p.Orientation)
            .SetValidator(new OrientationValidator());
    }
}

using FluentValidation;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class ConditionsFormValuesValidator : AbstractValidator<ConditionsFormValues>
{
    public ConditionsFormValuesValidator()
    {
        RuleFor(t => t.Name)
            .NotNull()
            .NotEmpty()
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH);

        RuleFor(t => t.Description)
            .MaximumLength(GeneralConsts.MAX_LENGTH);

        RuleFor(p => p.Temperature)
            .TemperatureCondition();

        RuleFor(p => p.Humidity)
            .HumidityCondition();

        RuleFor(p => p.Light)
            .LightCondition();

        RuleFor(p => p.Orientation)
            .OrientationAxisCondition();
    }
}

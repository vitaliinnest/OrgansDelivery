using FluentValidation;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class UpdateConditionsValidator
    : AbstractValidator<UpdateConditionsModel>
{
    public UpdateConditionsValidator()
    {
        RuleFor(t => t.Name)
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH)
            .When(t => t.Name != null);

        RuleFor(t => t.Description)
            .MaximumLength(GeneralConsts.MAX_LENGTH)
            .When(t => t.Description != null);

        RuleFor(p => p.Temperature)
            .TemperatureCondition()
            .When(t => t.Temperature != null);

        RuleFor(p => p.Humidity)
            .HumidityCondition()
            .When(t => t.Humidity != null);

        RuleFor(p => p.Light)
            .LightCondition()
            .When(t => t.Light != null);

        RuleFor(p => p.Orientation)
            .OrientationAxisCondition()
            .When(t => t.Orientation != null);
    }
}

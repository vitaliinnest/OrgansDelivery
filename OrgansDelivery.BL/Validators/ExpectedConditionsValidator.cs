using FluentValidation;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.BL.Validators;

public class ExpectedConditionsValidator : AbstractValidator<IWithExpectedConditions>
{
    public ExpectedConditionsValidator()
    {
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

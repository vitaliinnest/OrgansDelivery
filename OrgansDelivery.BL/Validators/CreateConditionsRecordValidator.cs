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

		RuleFor(o => o.Ort_x)
			.OrientationAxis();

		RuleFor(o => o.Ort_y)
			.OrientationAxis();
    }
}

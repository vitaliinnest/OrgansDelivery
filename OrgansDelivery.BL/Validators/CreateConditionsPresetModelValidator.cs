﻿using FluentValidation;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class CreateConditionsPresetModelValidator
    : AbstractValidator<CreateConditionsPresetModel>
{
    public CreateConditionsPresetModelValidator()
    {
        RuleFor(t => t.Name)
            .NotNull()
            .NotEmpty()
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH);

        RuleFor(t => t.Description)
            .MaximumLength(GeneralConsts.MAX_LENGTH);

        RuleFor(p => p.Temperature)
            .Temperature();

        RuleFor(p => p.Humidity)
            .Humidity();

        RuleFor(p => p.Light)
            .Light();

        RuleFor(p => p.OrientationLimits)
            .SetValidator(new OrientationLimitsValidator());
    }
}
﻿using FluentValidation;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.BL.Validators;

public class ConditionsValidator : AbstractValidator<IWithConditions>
{
    public ConditionsValidator()
    {
        RuleFor(p => p.Temperature)
            .Temperature();

        RuleFor(p => p.Humidity)
            .Humidity();

        RuleFor(p => p.Light)
            .Light();
    }
}

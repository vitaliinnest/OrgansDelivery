﻿using FluentValidation;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Validators;

public class OrientationLimitsValidator : AbstractValidator<OrientationLimits>
{
    public OrientationLimitsValidator()
    {
        RuleFor(l => l.XLimit)
            .OrientationAxis();

        RuleFor(l => l.YLimit)
            .OrientationAxis();
    }
}
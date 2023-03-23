﻿using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Validators;

public class CreateContainerModelValidator : AbstractValidator<CreateContainerModel>
{
    public CreateContainerModelValidator(UserManager<User> userManager)
    {
        RuleFor(c => c.Conditions)
            .SetValidator(new ExpectedConditionsValidator())
            .When(c => c.Conditions != null);

        // todo: other fields
    }
}

﻿using FluentValidation;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class OrganFormValuesValidator : AbstractValidator<OrganFormValues>
{
    public OrganFormValuesValidator()
    {
        RuleFor(t => t.Name)
            .NotNull()
            .NotEmpty()
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH);

        RuleFor(t => t.Description)
            .MaximumLength(GeneralConsts.MAX_LENGTH);

        RuleFor(t => t.OrganCreationDate)
            .NotNull()
            .OrganCreationDate();
    }
}

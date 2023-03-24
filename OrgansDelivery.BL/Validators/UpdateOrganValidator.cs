using FluentValidation;
using OrganStorage.DAL.Entities;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class UpdateOrganValidator
    : AbstractValidator<UpdateOrganModel>
{
    public UpdateOrganValidator()
    {
        RuleFor(t => t.Name)
            .Length(GeneralConsts.MIN_LENGTH,
                    GeneralConsts.MAX_LENGTH)
            .When(t => t.Name != null);

        RuleFor(t => t.Description)
            .MaximumLength(GeneralConsts.MAX_LENGTH)
            .When(t => t.Description != null);

        //RuleFor(t => t.OrganCreationDate)
        //    .OrganCreationDate()
        //    .When(t => t.OrganCreationDate != null);
    }
}

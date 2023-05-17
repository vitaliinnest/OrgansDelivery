using FluentValidation;
using OrganStorage.BL.Services;
using OrganStorage.BL.Extensions;
using OrganStorage.BL.Models;

namespace OrganStorage.BL.Validators;

public class InviteUserModelValidator : AbstractValidator<InviteFormValues>
{
    public InviteUserModelValidator(IRoleService roleService)
    {
        RuleFor(u => u.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}

using FluentValidation;
using OrganStorage.BL.Services;
using OrganStorage.BL.Extensions;
using OrganStorage.BL.Models;

namespace OrganStorage.BL.Validators;

public class InviteUserModelValidator : AbstractValidator<InviteUserModel>
{
    public InviteUserModelValidator(IRoleService roleService)
    {
        RuleFor(u => u.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.RoleId)
            .RoleId(roleService);
    }
}

using FluentValidation;
using OrgansDelivery.BL.Models;
using OrgansDelivery.BL.Services;
using OrgansDelivery.BL.Extensions;

namespace OrgansDelivery.BL.Validators;

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

using FluentValidation;
using OrgansDelivery.BL.Models;

namespace OrgansDelivery.BL.Validators;

public class InviteUserModelValidator : AbstractValidator<InviteUserModel>
{
    public InviteUserModelValidator()
    {
        RuleFor(u => u.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}

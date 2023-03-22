using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrgansDelivery.BL.Extensions;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Validators;

public class CreateContainerModelValidator : AbstractValidator<CreateContainerModel>
{
    public CreateContainerModelValidator(UserManager<User> userManager)
    {
        RuleFor(c => c.Conditions)
            .SetValidator(new ConditionsValidator())
            .When(c => c.Conditions != null);

        RuleFor(c => c.Password)
            .NotNull()
            .NotEmpty()
            .PasswordAsync(userManager);
    }
}

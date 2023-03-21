using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrgansDelivery.BL.Extensions;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator(UserManager<User> userManager)
    {
        RuleFor(u => u.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Password)
            .NotNull()
            .NotEmpty()
            .PasswordAsync(userManager);
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Entities.Auth;

namespace OrganStorage.BL.Validators;

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

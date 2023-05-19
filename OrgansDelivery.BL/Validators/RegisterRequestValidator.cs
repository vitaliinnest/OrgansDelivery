using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Extensions;
using OrganStorage.BL.Models.Auth;
using OrganStorage.DAL.Entities;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator(UserManager<User> userManager)
    {
        RuleFor(u => u.Name)
            .NotNull()
            .NotEmpty()
            .Matches(UserConsts.NAME_REGEX)
            .Length(UserConsts.NAME_MIN_LENGTH,
                    UserConsts.NAME_MAX_LENGTH);

        RuleFor(u => u.Surname)
            .NotNull()
            .NotEmpty()
            .Matches(UserConsts.NAME_REGEX)
            .Length(UserConsts.NAME_MIN_LENGTH,
                    UserConsts.NAME_MAX_LENGTH);

        RuleFor(u => u.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Password)
            .NotNull()
            .NotEmpty()
            .PasswordAsync(userManager);

        //RuleFor(u => u.RepeatPassword)
        //    .NotNull()
        //    .NotEmpty()
        //    .Equal(u => u.Password);
    }
}

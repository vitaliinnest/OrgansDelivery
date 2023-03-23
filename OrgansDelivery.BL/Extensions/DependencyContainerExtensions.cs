using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrganStorage.BL.MappingProfiles;
using OrganStorage.BL.Models.Options;
using OrganStorage.BL.Services;
using OrganStorage.BL.Validators;

namespace OrganStorage.BL.Extensions;

public static class DependencyContainerExtensions
{
    public static void RegisterBL(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
        services.AddAutoMapper(typeof(AuthMappingProfile));
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        services.AddScoped<IGenericValidator, GenericValidator>();
        services.AddScoped<IClaimsService, ClaimsService>();
        services.AddScoped<ITokenBuilder, TokenBuilder>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IInviteService, InviteService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailMessageBuilder, EmailMessageBuilder>();
        services.AddScoped<IContainerService, ContainerService>();
        services.AddScoped<IConditionPresetService, ConditionPresetService>();
        services.AddScoped<IOrganService, OrganService>();
        services.AddScoped<IConditionsHistoryService, ConditionsHistoryService>();
    }
}

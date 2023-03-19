using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrgansDelivery.BL.MappingProfiles;
using OrgansDelivery.BL.Models.Options;
using OrgansDelivery.BL.Services;

namespace OrgansDelivery.BL.Extensions;

public static class DependencyContainerExtensions
{
    public static void RegisterBL(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
        services.AddAutoMapper(typeof(AuthMappingProfile));
        services.AddScoped<IClaimsCalculator, ClaimsCalculator>();
        services.AddScoped<ITokenBuilder, TokenBuilder>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRolesService, RolesService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IInviteService, InviteService>();
        services.AddScoped<IEmailMessageBuilder, EmailMessageBuilder>();
    }
}

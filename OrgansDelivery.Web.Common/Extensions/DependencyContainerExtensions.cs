﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OrganStorage.BL.Models.Options;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace OrganStorage.Web.Common.Extensions;

public static class DependencyContainerExtensions
{
    public static void RegisterWeb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));

        services
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

        services.AddSwaggerGenNewtonsoftSupport();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddIdentityCore<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;

            options.Password = new PasswordOptions
            {
                RequiredLength = 8,
                RequireLowercase = true,
                RequireUppercase = false,
                RequireDigit = false,
                RequireNonAlphanumeric = false
            };
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders()
        .AddUserManager<UserManager<User>>()
        .AddSignInManager<SignInManager<User>>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                };
            });

        services.AddScoped<IEnvironmentProvider, EnvironmentProvider>();
        services.AddScoped<ITenantRequestResolver, TenantRequestResolver>();
        services.AddScoped<IUserRequestResolver, UserRequestResolver>();

        services.AddHttpContextAccessor();
        services.AddRouting(options => options.LowercaseUrls = true);
    }
}

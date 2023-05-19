using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using OrganStorage.DAL.Extensions;
using OrganStorage.BL.Extensions;
using OrganStorage.BL.Models.Options;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;
using OrganStorage.DAL.Data;
using OrganStorage.Web.Common.Middlewares;
using OrganStorage.Web.Common.Services;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDAL(builder.Configuration);
builder.Services.RegisterBL(builder.Configuration);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials()
			.WithOrigins("http://localhost:3000", "https://localhost:3000");
	});
});

builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    opts.ReplaceService<IModelCacheKeyFactory, MultiTenantModelCacheKeyFactory>();
});

builder.Services.AddIdentityCore<User>(options =>
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
        };
    });

builder.Services.AddScoped<IEnvironmentProvider, EnvironmentProvider>();
builder.Services.AddScoped<ITenantRequestResolver, TenantRequestResolver>();
builder.Services.AddScoped<IUserRequestResolver, UserRequestResolver>();
builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<MultitenancyDbContextMiddleware>();
app.UseMiddleware<EnvironmentMiddleware>();
app.MapControllers();

app.Run();

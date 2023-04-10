using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.Client;
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
        services.Configure<BrokerHostSettings>(configuration.GetSection("BrokerHostSettings"));
		services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
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
        services.AddScoped<IConditionsService, ConditionsService>();
        services.AddScoped<IOrganService, OrganService>();
        services.AddScoped<IConditionsHistoryService, ConditionsHistoryService>();
		services.AddMqttClientHostedService(configuration);
	}

	public static IServiceCollection AddMqttClientHostedService(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddMqttClientServiceWithConfig(aspOptionBuilder =>
		{
			var clientSettings = configuration.GetSection("ClientSettings").Get<ClientSettings>();
			var brokerHostSettings = configuration.GetSection("BrokerHostSettings").Get<BrokerHostSettings>();

			aspOptionBuilder
				.WithCredentials(clientSettings.UserName, clientSettings.Password)
				.WithClientId(clientSettings.Id)
				.WithTcpServer(brokerHostSettings.Host, brokerHostSettings.Port);
		});
		return services;
	}

	private static IServiceCollection AddMqttClientServiceWithConfig(
		this IServiceCollection services, Action<MqttClientOptionsBuilder> configure)
	{
		services.AddSingleton(serviceProvider =>
		{
			var optionBuilder = new MqttClientOptionsBuilder();
			configure(optionBuilder);
			return optionBuilder.Build();
		});
		services.AddSingleton<IHostedService, MqttClientService>();
		return services;
	}
}

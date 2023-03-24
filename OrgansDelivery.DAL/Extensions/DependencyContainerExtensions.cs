using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Services;

namespace OrganStorage.DAL.Extensions;

public static class DependencyContainerExtensions
{
    public static void RegisterDAL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEnvironmentProvider, EnvironmentProvider>();
        services.AddScoped<ITenantRepository, TenantRepository>();
    }
}

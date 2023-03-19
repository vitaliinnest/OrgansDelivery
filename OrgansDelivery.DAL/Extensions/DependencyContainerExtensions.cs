using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Services;

namespace OrgansDelivery.DAL.Extensions;

public static class DependencyContainerExtensions
{
    public static void RegisterDAL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddScoped<IEnvironmentProvider, EnvironmentProvider>();
        services.AddScoped<ITenantProvider, TenantProvider>();
    }
}

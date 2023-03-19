using Microsoft.EntityFrameworkCore;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Services;

namespace OrgansDelivery.Web.Extensions;

public static class DependencyContainerExtensions
{
    public static void RegisterWeb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddScoped<IEnvironmentProvider, EnvironmentProvider>();
    }
}

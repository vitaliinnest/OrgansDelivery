using Microsoft.Extensions.DependencyInjection;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.DAL.Services;

public static class EnvironmentContext
{
    public static void Set(Tenant tenant, IServiceProvider serviceProvider)
    {
        var tenantProvider = serviceProvider.GetService<IEnvironmentProvider>();
        tenantProvider.Tenant = tenant;
    }
}

using Microsoft.Extensions.DependencyInjection;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.DAL.Services;

public static class EnvironmentSetter
{
    public static void SetTenant(Tenant tenant, IServiceProvider serviceProvider)
    {
        var tenantProvider = serviceProvider.GetService<IEnvironmentProvider>();
        tenantProvider.Tenant = tenant;
    }

    public static void SetUser(User user, IServiceProvider serviceProvider)
    {
        var tenantProvider = serviceProvider.GetService<IEnvironmentProvider>();
        tenantProvider.User = user;
    }
}

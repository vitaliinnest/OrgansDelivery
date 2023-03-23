using Microsoft.Extensions.DependencyInjection;
using OrganStorage.DAL.Entities;

namespace OrganStorage.DAL.Services;

public static class EnvironmentSetter
{
    public static void SetTenant(Tenant tenant, IServiceProvider serviceProvider)
    {
        var tenantProvider = serviceProvider.GetService<IEnvironmentProvider>();
        tenantProvider.Tenant = tenant;
    }

    public static void SetUser(User user, IServiceProvider serviceProvider)
    {
        var environmentProvider = serviceProvider.GetService<IEnvironmentProvider>();
        environmentProvider.User = user;
    }
}

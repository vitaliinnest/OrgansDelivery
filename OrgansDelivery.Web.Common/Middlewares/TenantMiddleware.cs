using Microsoft.AspNetCore.Http;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Services;

namespace OrganStorage.Web.Common.Middlewares;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext context,
        IDbContextTenantEnvironmentProvider environmentProvider,
        ITenantRequestResolver tenantRequestResolver,
        IServiceProvider provider)
    {
        var tenant = tenantRequestResolver.ResolveTenant(environmentProvider.TenantId);
        if (tenant != null)
        {
            EnvironmentSetter.SetTenant(tenant, provider);
        }

        await _next(context);
    }
}

using OrgansDelivery.DAL.Services;
using OrgansDelivery.Web.Services;

namespace OrgansDelivery.Web.Middlewares;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITenantRequestResolver _tenantResolver;

    public TenantMiddleware(
        RequestDelegate next,
        ITenantRequestResolver tenantResolver
        )
    {
        _next = next;
        _tenantResolver = tenantResolver;
    }

    public async Task Invoke(HttpContext context, IServiceProvider provider)
    {
        var tenant = _tenantResolver.ResolveTenant(context.Request, context.User);
        if (tenant != null)
        {
            EnvironmentContext.Set(tenant, provider);
        }

        await _next(context);
    }
}

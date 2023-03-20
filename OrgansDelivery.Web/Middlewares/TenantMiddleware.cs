using OrgansDelivery.DAL.Services;
using OrgansDelivery.Web.Services;

namespace OrgansDelivery.Web.Middlewares;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    
    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITenantRequestResolver tenantRequestResolver, IServiceProvider provider)
    {
        var tenant = tenantRequestResolver.ResolveTenant(context.Request, context.User);
        if (tenant != null)
        {
            EnvironmentSetter.SetTenant(tenant, provider);
        }

        await _next(context);
    }
}

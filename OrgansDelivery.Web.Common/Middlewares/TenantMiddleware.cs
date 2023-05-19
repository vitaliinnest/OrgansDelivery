using Microsoft.AspNetCore.Http;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Services;
using System.Security.Claims;

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
        IServiceProvider provider)
    {
		var tenantIdString = context?.User.FindFirstValue("tenantId");

        var tenantId = Guid.TryParse(tenantIdString, out var _tenantId)
            ? _tenantId
            : Guid.Empty;

        EnvironmentSetter.SetTenant(new() { Id = tenantId }, provider);

        await _next(context);
    }
}

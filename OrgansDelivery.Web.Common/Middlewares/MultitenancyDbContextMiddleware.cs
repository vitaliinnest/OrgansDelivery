using Microsoft.AspNetCore.Http;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Common.Middlewares;

public class MultitenancyDbContextMiddleware
{
    private readonly RequestDelegate _next;

    public MultitenancyDbContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext context,
        IServiceProvider provider)
    {
		var tenantId = context.FindUserClaimGuidValue("tenantId");
		EnvironmentSetter.SetTenant(new() { Id = tenantId }, provider);
        await _next(context);
    }
}

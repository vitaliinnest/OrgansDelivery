using Microsoft.AspNetCore.Http;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Extensions;
using OrganStorage.Web.Common.Services;
using System.Security.Claims;

namespace OrganStorage.Web.Common.Middlewares;

public class EnvironmentMiddleware
{
    private readonly RequestDelegate _next;

    public EnvironmentMiddleware(RequestDelegate next)
    {
        _next = next;
    }

	public async Task Invoke(
		HttpContext context,
		IUserRequestResolver userRequestResolver,
		ITenantRequestResolver tenantRequestResolver,
		IServiceProvider serviceProvider)
	{
		var userId = context.FindUserClaimGuidValue(ClaimTypes.NameIdentifier);
		var user = userRequestResolver.ResolveUser(userId);

		var tenantId = context.FindUserClaimGuidValue("tenantId");
		var tenant = tenantRequestResolver.ResolveTenant(tenantId);

		EnvironmentSetter.SetEnvironment(user, tenant, serviceProvider);
		
		await _next(context);
	}
}

using Microsoft.AspNetCore.Http;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Services;
using System.Security.Claims;

namespace OrganStorage.Web.Common.Middlewares;

public class UserMiddleware
{
    private readonly RequestDelegate _next;

    public UserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext context,
        IServiceProvider provider)
    {
		var userIdString = context?.User.FindFirstValue(ClaimTypes.NameIdentifier);
		var userId = Guid.TryParse(userIdString, out var _userId)
            ? _userId
            : Guid.Empty;

        EnvironmentSetter.SetUser(new() { Id = userId }, provider);

        await _next(context);
    }
}

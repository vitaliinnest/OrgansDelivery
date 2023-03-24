using Microsoft.AspNetCore.Http;
using OrganStorage.DAL.Services;
using OrganStorage.Web.Common.Services;

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
        IDbContextTenantEnvironmentProvider environmentProvider,
        IUserRequestResolver userRequestResolver,
        IServiceProvider provider)
    {
        var user = userRequestResolver.ResolveUser(environmentProvider.UserId);
        if (user != null)
        {
            EnvironmentSetter.SetUser(user, provider);
        }

        await _next(context);
    }
}

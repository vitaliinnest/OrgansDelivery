using OrgansDelivery.DAL.Services;
using OrgansDelivery.Web.Services;

namespace OrgansDelivery.Web.Middlewares;

public class UserMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserRequestResolver _userRequestResolver;

    public UserMiddleware(RequestDelegate next, IUserRequestResolver userRequestResolver)
    {
        _next = next;
        _userRequestResolver = userRequestResolver;
    }

    public async Task Invoke(HttpContext context, IServiceProvider provider)
    {
        var user = await _userRequestResolver.ResolveUserAsync(context.User);
        if (user == null)
        {
            await _next(context);
            return;
        }

        EnvironmentSetter.SetUser(user, provider);
        await _next(context);
    }
}

﻿using OrgansDelivery.DAL.Services;
using OrgansDelivery.Web.Services;

namespace OrgansDelivery.Web.Middlewares;

public class UserMiddleware
{
    private readonly RequestDelegate _next;
    
    public UserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserRequestResolver userRequestResolver, IServiceProvider provider)
    {
        var user = await userRequestResolver.ResolveUserAsync(context.User);
        if (user == null)
        {
            await _next(context);
            return;
        }

        EnvironmentSetter.SetUser(user, provider);
        await _next(context);
    }
}

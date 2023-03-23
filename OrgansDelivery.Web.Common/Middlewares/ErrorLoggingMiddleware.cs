﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrganStorage.Web.Common.Extensions;

namespace OrganStorage.Web.Common.Middlewares;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorLoggingMiddleware> _logger;

    public ErrorLoggingMiddleware(
        RequestDelegate next,
        ILogger<ErrorLoggingMiddleware> logger
        )
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) when (_logger.LogException(ex))
        {
            //never gets here as when closure yields false
        }
    }
}

using FluentResults;
using Microsoft.AspNetCore.Mvc;
using OrganStorage.BL.Extensions;

namespace OrganStorage.Web.Common.Extensions;

public static class ControllerBaseExtensions
{
    public static ActionResult<T> ToActionResult<T>(this ControllerBase controllerBase, Result<T> result)
    {
        if (result.IsFailed)
        {
            return controllerBase.BadRequest(result.ErrorMessagesToString());
        }

        return controllerBase.Ok(result.Value);
    }

    public static ActionResult ToActionResult(this ControllerBase controllerBase, Result result)
    {
        if (result.IsFailed)
        {
            return controllerBase.BadRequest(result.ErrorMessagesToString());
        }

        return controllerBase.Ok();
    }
}

﻿using Microsoft.AspNetCore.Identity;

namespace OrgansDelivery.BL.Extensions;

public static class IdentityResultExtensions
{
    public static string ErrorsToString(this IdentityResult identityResult)
    {
        return string.Join("; ", identityResult.Errors.Select(e => $"{e.Code}: {e.Description}"));
    }
}

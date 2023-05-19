using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace OrganStorage.Web.Common.Extensions;

public static class HttpContextExtensions
{
	public static Guid FindUserClaimGuidValue(this HttpContext context, string claimType)
	{
		var claimString = context?.User.FindFirstValue(claimType);
		_ = Guid.TryParse(claimString, out var guidValue);
		return guidValue;
	}
}

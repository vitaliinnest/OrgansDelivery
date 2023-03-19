using OrgansDelivery.Web.Consts;

namespace OrgansDelivery.Web.Extensions;

public static class PathStringExtensions
{
    public static string ExtractTenantUrl(this PathString pathString)
    {
        var urlPart = pathString.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault(_ => _.StartsWith(RoutingConsts.TenantUrlPrefix));

        return !string.IsNullOrEmpty(urlPart)
            ? urlPart[RoutingConsts.TenantUrlPrefix.Length..].ToLowerInvariant()
            : null;
    }
}

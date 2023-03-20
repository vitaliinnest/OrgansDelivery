using Microsoft.AspNetCore.Http;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.DAL.Services;
using OrgansDelivery.Web.Common.Extensions;
using OrgansDelivery.Web.Consts;
using System.Security.Claims;

namespace OrgansDelivery.Web.Common.Services;

public interface ITenantRequestResolver
{
    Tenant ResolveTenant(HttpRequest request, ClaimsPrincipal user);
}

public class TenantRequestResolver : ITenantRequestResolver
{
    private readonly ITenantRepository _tenantProvider;

    public TenantRequestResolver(
        ITenantRepository tenantProvider
        )
    {
        _tenantProvider = tenantProvider;
    }

    public Tenant ResolveTenant(HttpRequest request, ClaimsPrincipal user)
    {
        var tenantUrl = request.Path.ExtractTenantUrl();
        var tenantIdStr = request.Headers[HttpHeaders.TenantId].FirstOrDefault();

        if (!string.IsNullOrEmpty(tenantUrl))
        {
            return _tenantProvider.GetTenantByUrl(tenantUrl);
        }

        if (!string.IsNullOrEmpty(tenantIdStr) && Guid.TryParse(tenantIdStr, out var tenantId))
        {
            return _tenantProvider.GetTenantById(tenantId);
        }

        // todo: may not work
        return null;
    }
}

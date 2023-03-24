using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;
using System.Security.Claims;

namespace OrganStorage.Web.Common.Services;

public interface ITenantRequestResolver
{
    Guid? ResolveTenantId(ClaimsPrincipal user);
    Tenant ResolveTenant(Guid tenantId);
}

public class TenantRequestResolver : ITenantRequestResolver
{
    private readonly ITenantRepository _tenantProvider;

    public TenantRequestResolver(
        ITenantRepository tenantProvider)
    {
        _tenantProvider = tenantProvider;
    }

    public Guid? ResolveTenantId(ClaimsPrincipal user)
    {
        var tenantIdStr = user.FindFirstValue("tenantId");
        if (tenantIdStr == null)
        {
            return null;
        }
        return Guid.Parse(tenantIdStr);
    }

    public Tenant ResolveTenant(Guid tenantId)
    {
        return _tenantProvider.GetTenantById(tenantId);
    }
}

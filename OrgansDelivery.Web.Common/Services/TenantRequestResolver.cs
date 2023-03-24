using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;

namespace OrganStorage.Web.Common.Services;

public interface ITenantRequestResolver
{
    Tenant ResolveTenant(Guid tenantId);
}

public class TenantRequestResolver : ITenantRequestResolver
{
    private readonly ITenantRepository _tenantRepository;

    public TenantRequestResolver(
        ITenantRepository tenantProvider)
    {
        _tenantRepository = tenantProvider;
    }

    public Tenant ResolveTenant(Guid tenantId)
    {
        return _tenantRepository.GetTenantById(tenantId);
    }
}

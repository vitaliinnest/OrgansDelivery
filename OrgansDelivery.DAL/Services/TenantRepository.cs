using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.DAL.Services;

public interface ITenantRepository
{
    Tenant GetTenantById(Guid tenantId);
    Tenant GetTenantByUrl(string tenantUrl);
}

public class TenantRepository : ITenantRepository
{
    private readonly AppDbContext _appDbContext;

    public TenantRepository(
        AppDbContext appDbContext
        )
    {
        _appDbContext = appDbContext;
    }

    public Tenant GetTenantById(Guid tenantId)
    {
        return FindTenant(t => t.Id == tenantId);
    }

    public Tenant GetTenantByUrl(string tenantUrl)
    {
        return FindTenant(t => t.Url == tenantUrl);
    }

    private Tenant FindTenant(Func<Tenant, bool> predicate)
    {
        return _appDbContext.Tenants.FirstOrDefault(predicate);
    }
}

using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.DAL.Services;

public interface ITenantProvider
{
    Tenant GetTenantById(Guid tenantId);
    Tenant GetTenantByUrl(string tenantUrl);
}

public class TenantProvider : ITenantProvider
{
    private readonly AppDbContext _appDbContext;

    public TenantProvider(
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

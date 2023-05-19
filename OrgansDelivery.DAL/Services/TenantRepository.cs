using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.DAL.Services;

public interface ITenantRepository
{
    Tenant GetTenantById(Guid tenantId);
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
		return _appDbContext.Tenants.FirstOrDefault(t => t.Id == tenantId);
    }
}

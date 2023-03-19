using Microsoft.EntityFrameworkCore;
using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Extensions;

public static class MarkDataKeyExtension
{
    public static void SetTenantIdIfNeeded(this DbContext context, Guid tenantId)
    {
        foreach (var entityEntry in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            if (entityEntry.Entity is IMustHaveTenant hasDataKey && hasDataKey.TenantId == Guid.Empty)
            {
                hasDataKey.TenantId = tenantId;
            }
        }
    }
}

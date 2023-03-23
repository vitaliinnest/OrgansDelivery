using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Extensions;

public static class MarkTenantExtensions
{
    public static void SetTenantIdIfNeeded(this DbContext context, Guid tenantId)
    {
        foreach (var entityEntry in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            if (entityEntry.Entity is IMustHaveTenant mustHaveTenant
                && mustHaveTenant.TenantId == Guid.Empty
                && tenantId != Guid.Empty)
            {
                mustHaveTenant.TenantId = tenantId;
            }
        }
    }
}

using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Extensions;

public static class TenantQueryExtension
{
    public static void AddTenantQueryFilter(this IMutableEntityType entityData, Guid tenantId, bool adminMode)
    {
        var methodToCall = typeof(TenantQueryExtension)
            .GetMethod(nameof(SetupSingleTenantQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { tenantId, adminMode });
        entityData.SetQueryFilter((LambdaExpression)filter);
    }

    private static LambdaExpression SetupSingleTenantQueryFilter<TEntity>(Guid tenantId, bool adminMode)
        where TEntity : class, IMustHaveTenant
    {
        Expression<Func<TEntity, bool>> filter = x => adminMode || x.TenantId == tenantId;
        return filter;
    }
}

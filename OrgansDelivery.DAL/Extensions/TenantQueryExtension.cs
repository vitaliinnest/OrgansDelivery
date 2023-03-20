using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Extensions;

public static class TenantQueryExtension
{
    public static void AddTenantQueryFilter(this IMutableEntityType entityData, Guid tenantId)
    {
        var methodToCall = typeof(TenantQueryExtension)
            .GetMethod(nameof(SetupSingleTenantQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { tenantId });
        entityData.SetQueryFilter((LambdaExpression)filter);
    }

    private static LambdaExpression SetupSingleTenantQueryFilter<TEntity>(Guid tenantId)
        where TEntity : class, IMustHaveTenant
    {
        Expression<Func<TEntity, bool>> filter = x => x.TenantId == tenantId;
        return filter;
    }
}

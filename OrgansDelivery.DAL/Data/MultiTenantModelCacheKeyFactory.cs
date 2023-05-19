using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace OrganStorage.DAL.Data;

public class MultiTenantModelCacheKeyFactory : IModelCacheKeyFactory
{
	public object Create(DbContext dbContext, bool designTime)
	{
		if (dbContext is AppDbContext tenantDb)
		{
			return (dbContext.GetType(), tenantDb.TenantId, designTime);
		}

		return (dbContext.GetType(), designTime);
	}
}

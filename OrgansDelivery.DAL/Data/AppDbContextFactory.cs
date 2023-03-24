using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Services;

namespace OrganStorage.DAL.Data;

public class AppDbContextFactory : IDbContextFactory<AppDbContext>
{
    private readonly IDbContextFactory<AppDbContext> _pooledFactory;
    private readonly IDbContextTenantEnvironmentProvider _contextProvider;

    public AppDbContextFactory(
        IDbContextFactory<AppDbContext> pooledFactory,
        IDbContextTenantEnvironmentProvider contextProvider)
    {
        _pooledFactory = pooledFactory;
        _contextProvider = contextProvider;
    }

    public AppDbContext CreateDbContext()
    {
        var context = _pooledFactory.CreateDbContext();
        context.TenantId = _contextProvider.TenantId;
        return context;
    }
}

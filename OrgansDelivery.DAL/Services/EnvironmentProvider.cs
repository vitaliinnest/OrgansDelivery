using OrganStorage.DAL.Entities;

namespace OrganStorage.DAL.Services;

public interface IEnvironmentProvider
{
    Tenant Tenant { get; set; }
    User User { get; set; }
}

public class EnvironmentProvider : IEnvironmentProvider
{
    public Tenant Tenant { get; set; }
    public User User { get; set; }
}

public interface IDbContextTenantEnvironmentProvider
{
    Guid UserId { get; set; }
    Guid TenantId { get; set; }
}

public class DbContextTenantEnvironmentProvider : IDbContextTenantEnvironmentProvider
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
}

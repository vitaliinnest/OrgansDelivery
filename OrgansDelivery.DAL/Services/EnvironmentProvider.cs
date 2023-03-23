using OrganStorage.DAL.Entities;

namespace OrganStorage.DAL.Services;

public interface IEnvironmentProvider
{
    public Tenant Tenant { get; set; }
    public User User { get; set; }
}

public class EnvironmentProvider : IEnvironmentProvider
{
    public Tenant Tenant { get; set; }
    public User User { get; set; }
}

using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.DAL.Services;

public interface IEnvironmentProvider
{
    public Tenant Tenant { get; set; }
}

public class EnvironmentProvider : IEnvironmentProvider
{
    public Tenant Tenant { get; set; }
}

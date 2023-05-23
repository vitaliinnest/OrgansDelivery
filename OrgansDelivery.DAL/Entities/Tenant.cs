using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Tenant : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class CreateTenantDto
{
    public Tenant Tenant { get; set; }
	public string Token { get; set; }
}

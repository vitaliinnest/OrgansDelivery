using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Tenant : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

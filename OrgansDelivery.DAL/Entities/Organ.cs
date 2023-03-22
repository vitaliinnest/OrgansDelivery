using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Entities;

public class Organ : IEntity, IMustHaveTenant, IWithName, IWithDescription
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OrganCreationDate { get; set; }
}

public class CreateOrganModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OrganCreationDate { get; set; }
}

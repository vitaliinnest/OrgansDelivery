using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Entities;

public class Container : IEntity, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Conditions Conditions { get; set; }
    // todo: hash password
    public string Password { get; set; }
    public bool IsLocked { get; set; }
    // todo: configure one to one relation
    public Guid? OrganId { get; set; }
}

public class CreateContainerModel
{
    public Guid? ConditionPresetId { get; set; }
    public Conditions Conditions { get; set; }
    public string Password { get; set; }
}

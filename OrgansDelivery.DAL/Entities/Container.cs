using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Container : IEntity, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Conditions Conditions { get; set; }
    public int ConditionsIntervalCheckInSecs { get; set; }
    // todo: hash password
    public string Password { get; set; }
    public bool IsLocked { get; set; }
    // todo: configure one to one relation
    public Guid? OrganId { get; set; }
    public Organ Organ { get; set; }
    public bool IsForOrder { get; set; }
}

public class CreateContainerModel
{
    public Guid? ConditionPresetId { get; set; }
    public Conditions Conditions { get; set; }
    public string Password { get; set; }
}

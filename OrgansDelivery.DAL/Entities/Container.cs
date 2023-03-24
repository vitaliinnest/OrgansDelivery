using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Container : IEntity, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public int ConditionsIntervalCheckInSecs { get; set; }
    public Guid? OrganId { get; set; }
    public Organ Organ { get; set; }
    public ICollection<ConditionsRecord> ConditionsHistory { get; set; }
    public Guid ConditionsId { get; set; }
    public Conditions Conditions { get; set; }
}

public class CreateContainerModel
{
    public Guid ConditionsId { get; set; }
    public int ConditionsIntervalCheckInSecs { get; set; }
}

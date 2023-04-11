using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Container : IEntity, IMustHaveTenant, IWithName, IWithDescription
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? OrganId { get; set; }
    public Organ Organ { get; set; }
    public ICollection<ConditionsRecord> Records { get; set; }
    public Guid ConditionsId { get; set; }
    public Conditions Conditions { get; set; }
    public Guid DeviceId { get; set; }
    public Device Device { get; set; }
}

public class CreateContainerModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ConditionsId { get; set; }
    public Guid? OrganId { get; set; }
}

public class UpdateContainerModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? ConditionsId { get; set; }
}

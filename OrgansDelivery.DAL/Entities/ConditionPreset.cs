using Microsoft.EntityFrameworkCore;
using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Entities;

// apply conditions to a container (basically - copy them to Conditions property)
[PrimaryKey(nameof(Id), nameof(Name))]
public class ConditionPreset : Conditions, IEntity, IMustHaveTenant, IWithName, IWithDescription
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class CreateConditionsPresetModel : Conditions
{
    public string Name { get; set; }
    public string Description { get; set; }
}

using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

// apply conditions to a container (basically - copy them to Conditions property)
[PrimaryKey(nameof(Id), nameof(Name))]
public class ConditionPreset : IEntity, IMustHaveTenant,
    IWithName, IWithDescription, IWithConditions, IWithOrientationLimits
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Humidity { get; set; }
    public decimal Light { get; set; }
    public decimal Temperature { get; set; }
    public OrientationLimits OrientationLimits { get; set; }
}

public class CreateConditionsPresetModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Temperature { get; set; }
    public decimal Humidity { get; set; }
    public decimal Light { get; set; }
    public OrientationLimits OrientationLimits { get; set; }
}

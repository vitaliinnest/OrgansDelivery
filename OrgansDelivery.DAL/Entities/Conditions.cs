using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

// apply conditions to a container (basically - copy them to Conditions property)
public class Conditions : IEntity, IMustHaveTenant,
    IWithName, IWithDescription, IWithExpectedConditions
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Condition<decimal> Humidity { get; set; }
    public Condition<decimal> Light { get; set; }
    public Condition<decimal> Temperature { get; set; }
    public Condition<Orientation> Orientation { get; set; }
    public ICollection<Container> Containers { get; set; }
}

[Owned]
public class Condition<T>
{
    public T ExpectedValue { get; set; }
    public T AllowedDeviation { get; set; }
}

public class CreateConditionsPresetModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Condition<decimal> Humidity { get; set; }
    public Condition<decimal> Light { get; set; }
    public Condition<decimal> Temperature { get; set; }
    public Condition<Orientation> Orientation { get; set; }
}

using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Conditions : IEntity, IMustHaveTenant,
    IWithName, IWithDescription, IWithExpectedConditions
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
	// IsArhival is true if conditions is deleted or edited
	public bool IsArchival { get; set; }
	public Condition<decimal> Humidity { get; set; }
    public Condition<decimal> Light { get; set; }
    public Condition<decimal> Temperature { get; set; }
    public Condition<Orientation> Orientation { get; set; }
    
    public ICollection<Organ> Organs { get;set; }
	public ICollection<ConditionsRecord> Records { get; set; }
}

public class ConditionsRef
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public Condition<decimal> Humidity { get; set; }
	public Condition<decimal> Light { get; set; }
	public Condition<decimal> Temperature { get; set; }
	public Condition<Orientation> Orientation { get; set; }
}

public class ConditionsDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public Condition<decimal> Humidity { get; set; }
	public Condition<decimal> Light { get; set; }
	public Condition<decimal> Temperature { get; set; }
	public Condition<Orientation> Orientation { get; set; }
}

public class ConditionsFormValues
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Condition<decimal> Humidity { get; set; }
    public Condition<decimal> Light { get; set; }
    public Condition<decimal> Temperature { get; set; }
    public Condition<Orientation> Orientation { get; set; }
	
    public bool ConditionsChanged(Conditions conditions) =>
           !Humidity.Equals(conditions.Humidity)
        || !Light.Equals(conditions.Light)
        || !Orientation.Equals(conditions.Orientation);
}

[Owned]
public class Condition<T> : IEquatable<Condition<T>>
	where T : IEquatable<T>
{
	public T ExpectedValue { get; set; }
	public T AllowedDeviation { get; set; }

	public bool Equals(Condition<T> condition)
	{
		return ExpectedValue.Equals(condition.ExpectedValue)
			&& AllowedDeviation.Equals(condition.AllowedDeviation);
	}
}

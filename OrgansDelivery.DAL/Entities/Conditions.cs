﻿using Microsoft.EntityFrameworkCore;
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
}

[Owned]
public class Condition<T>
{
    public T ExpectedValue { get; set; }
    public T AllowedDeviation { get; set; }
}

public class CreateConditionsModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Condition<decimal> Humidity { get; set; }
    public Condition<decimal> Light { get; set; }
    public Condition<decimal> Temperature { get; set; }
    public Condition<Orientation> Orientation { get; set; }
}

public class UpdateConditionsModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Condition<decimal> Humidity { get; set; }
    public Condition<decimal> Light { get; set; }
    public Condition<decimal> Temperature { get; set; }
    public Condition<Orientation> Orientation { get; set; }
    
    public bool AreConditionsUpdated() =>
        Humidity != null || Light != null || Orientation != null;
}

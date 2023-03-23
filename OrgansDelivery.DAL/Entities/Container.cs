﻿using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Container : IEntity, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public ExpectedConditions Conditions { get; set; }
    public int ConditionsIntervalCheckInSecs { get; set; }
    // todo: configure one to one relation
    public Guid? OrganId { get; set; }
    public Organ Organ { get; set; }
}

public class CreateContainerModel
{
    public Guid? ConditionPresetId { get; set; }
    public ExpectedConditions Conditions { get; set; }
    public int ConditionsIntervalCheckInSecs { get; set; }
}

﻿namespace OrganStorage.DAL.Interfaces;

public interface IMustHaveTenant
{
    public Guid TenantId { get; set; }
}

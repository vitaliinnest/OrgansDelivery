﻿using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Invite : IEntity, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Email { get; set; }
    public Guid InviteCode { get; set; } = Guid.NewGuid();
}

using Microsoft.AspNetCore.Identity;
using OrgansDelivery.DAL.Enums;
using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Entities;

public class Invite : IEntity, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Email { get; set; }
    public Guid InviteCode { get; set; } = Guid.NewGuid();
    public Guid RoleId { get; set; }
    public Language Language { get; set; }
}

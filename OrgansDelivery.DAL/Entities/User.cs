using Microsoft.AspNetCore.Identity;
using OrgansDelivery.DAL.Enums;
using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Entities;

public class User : IdentityUser<Guid>, IMustHaveTenant
{
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Language Language { get; set; }
}

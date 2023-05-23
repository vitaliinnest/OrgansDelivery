using Microsoft.AspNetCore.Identity;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class User : IdentityUser<Guid>, IMustHaveTenant
{
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class UserFormValues
{
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Tenant Tenant { get; set; }
    public RoleDto Role { get; set; }
}

using Microsoft.AspNetCore.Identity;
using OrganStorage.DAL.Enums;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class User : IdentityUser<Guid>, IMustHaveTenant
{
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Language Language { get; set; }
}

// todo
public class UserDto
{

}

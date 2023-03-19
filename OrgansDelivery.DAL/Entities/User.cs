using Microsoft.AspNetCore.Identity;
using OrgansDelivery.DAL.Enums;

namespace OrgansDelivery.DAL.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public Language Language { get; set; }
    public string PhotoUrl { get; set; }
}

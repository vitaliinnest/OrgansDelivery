using OrgansDelivery.DAL.Enums;

namespace OrgansDelivery.BL.Models.Auth;

public class LoginResponse
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhotoUrl { get; set; }
    public Language Language { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
}

using OrgansDelivery.DAL.Enums;

namespace OrgansDelivery.BL.Models.Auth;

public class RegisterResponse
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public Language Language { get; set; }
    public string PhotoUrl { get; set; }
}

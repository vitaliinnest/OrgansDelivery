using OrgansDelivery.DAL.Enums;

namespace OrgansDelivery.BL.Models.Auth;

public class RegisterRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
    public Language Language { get; set; }
    public string PhotoUrl { get; set; }
    public Guid? InviteCode { get; set; }
}

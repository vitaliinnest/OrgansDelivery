using OrganStorage.DAL.Enums;

namespace OrganStorage.BL.Models.Auth;

public class RegisterResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public Language Language { get; set; }
}

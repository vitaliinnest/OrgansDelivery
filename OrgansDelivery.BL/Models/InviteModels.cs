using OrganStorage.DAL.Enums;

namespace OrganStorage.BL.Models;

public class InviteUserModel
{
    public string Email { get; set; }
    public Guid RoleId { get; set; }
    public Language Language { get; set; } = Language.English;
}

using OrgansDelivery.DAL.Enums;

namespace OrgansDelivery.BL.Models;

public class InviteUserModel
{
    public string Email { get; set; }
    public Guid RoleId { get; set; }
    public Language Language { get; set; } = Language.English;
}

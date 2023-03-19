using System.ComponentModel.DataAnnotations;

namespace OrgansDelivery.BL.Models.Auth;

public class LoginRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Email { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}

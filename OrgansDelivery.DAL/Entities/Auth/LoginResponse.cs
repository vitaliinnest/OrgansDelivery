namespace OrganStorage.DAL.Entities.Auth;

public class LoginResponse
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public Guid? RoleId { get; set; }
	public string RoleName { get; set; }
	public string Token { get; set; }
}

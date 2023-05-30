namespace OrganStorage.DAL.Entities.Auth;

public class RegisterResponse
{
	public string Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public bool EmailConfirmed { get; set; }
}

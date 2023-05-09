using OrganStorage.DAL.Enums;

namespace OrganStorage.DAL.Entities;

public class EmployeeDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public Language Language { get; set; }
	public RoleDto Role { get; set; }
}

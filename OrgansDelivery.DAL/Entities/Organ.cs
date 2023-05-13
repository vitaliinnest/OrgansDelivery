using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Organ : IEntity, IMustHaveTenant, IWithName, IWithDescription
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OrganCreationDate { get; set; }
    
    public Guid ContainerId { get; set; }
    public Container Container { get; set; }
    
    public Guid ConditionsId { get; set; }
    public Conditions Conditions { get; set; }

    public ICollection<ConditionsRecord> Records { get; set; }
}

public class OrganDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime OrganCreationDate { get; set; }
	public Guid ContainerId { get; set; }
	public Guid ConditionsId { get; set; }
}

public class OrganFormValues
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OrganCreationDate { get; set; }
    public Guid ContainerId { get; set; }
	public Guid ConditionsId { get; set; }
}

using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Container : IEntity, IMustHaveTenant, IWithName, IWithDescription
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public Organ Organ { get; set; }
    
    public Guid DeviceId { get; set; }
    public Device Device { get; set; }
}

public class ContainerRef
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
    public Guid OrganId { get; set; }
	public Guid DeviceId { get; set; }
}

public class ContainerDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public OrganRef Organ { get; set; }
	public DeviceRef Device { get; set; }
}

public class ContainerFormValues
{
    public string Name { get; set; }
    public string Description { get; set; }
	public Guid DeviceId { get; set; }
}

using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Device : IEntity, IMustHaveTenant, IWithName
{
	public Guid Id { get; set; }
	public Guid TenantId { get; set; }
	public string Name { get; set; }
	public int ConditionsIntervalCheckInMs { get; set; }
	
	public Container Container { get; set; }
}

public class DeviceRef
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public int ConditionsIntervalCheckInMs { get; set; }
	public Guid ContainerId { get; set; }
}

public class DeviceDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public int ConditionsIntervalCheckInMs { get; set; }
	public ContainerRef Container { get; set; }
}

public class DeviceFormValues
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public int ConditionsIntervalCheckInMs { get; set; }
}

public class DeviceConfigurationMessage
{
	public int Interval_ms { get; set; }
}

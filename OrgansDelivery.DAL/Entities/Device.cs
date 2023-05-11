using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Device : IEntity, IMustHaveTenant, IWithName, IWithDescription
{
	public Guid Id { get; set; }
	public Guid TenantId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public int ConditionsIntervalCheckInMs { get; set; }
	
	public Guid? ContainerId { get; set; }
	public Container Container { get; set; }
}

public class AddDeviceModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public Guid ContainerId { get; set; }
	public int ConditionsIntervalCheckInMs { get; set; }
}

//public class UpdateDeviceModel
//{
//	public string Name { get; set; }
//	public string Description { get; set; }
//	public Guid ContainerId { get; set; }
//}

public class UpdateDeviceConfigurationModel
{
	public int ConditionsIntervalCheckInMs { get; set; }
}

public class DeviceConfigurationMessage
{
	public int Interval_ms { get; set; }
}

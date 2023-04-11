using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class ConditionsRecord : IEntity, IMustHaveTenant, IWithOrientation
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Temperature { get; set; }
    public decimal Humidity { get; set; }
    public decimal Light { get; set; }
    public Orientation Orientation { get; set; }
    public Guid ContainerId { get; set; }
    public Container Container { get; set; }
}

public class ConditionsRecordDto
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid ContainerId { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Temperature { get; set; }
    public decimal Humidity { get; set; }
    public decimal Light { get; set; }
    public Orientation Orientation { get; set; }
}

public class CreateConditionsRecordModel
{
    public Guid Device_id { get; set; }
	public decimal Temperature { get; set; }
    public decimal Humidity { get; set; }
    public decimal Light { get; set; }
    public decimal Ort_x { get; set; }
    public decimal Ort_y { get; set; }
    public DateTime Sent_at_utc { get; set; }
}

public class GetConditionsHistoryModel
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class ContainerConditionsRecord : IEntity, IMustHaveTenant, IWithOrientation
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public DateTime DateTime { get; set; }
    // todo: configure relation
    public Guid ContainerId { get; set; }
    public decimal Temperature { get; set; }
    public decimal Humidity { get; set; }
    public decimal Light { get; set; }
    public Orientation Orientation { get; set; }
}

public class CreateConditionsRecordModel
{
    public DateTime DateTime { get; set; }
    public decimal Temperature { get; set; }
    public decimal Humidity { get; set; }
    public decimal Light { get; set; }
    public Orientation Orientation { get; set; }
}

public class GetConditionsHistoryModel
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

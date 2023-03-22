using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Entities;

// used for storing container history, so actual container conditions
// recorded every 5 minutes (or so)
// todo: allow to configure records interval
public class ContainerConditionsRecord : IEntity, IMustHaveTenant, IWithConditions, IWithOrientation
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Humidity { get; set; }
    public decimal Light { get; set; }
    public decimal Temperature { get; set; }
    public OrientationLimits OrientationLimits { get; set; }
}

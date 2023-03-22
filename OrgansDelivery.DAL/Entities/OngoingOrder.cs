using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Entities;

// order that is accepted and being processed (at any stage)
public class OngoingOrder : IEntity, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid BuyerId { get; set; }
    public Guid SellerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? CompleteTime { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsCompleted() => IsCanceled || CompleteTime.HasValue;
}

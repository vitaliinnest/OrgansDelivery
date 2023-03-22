using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Entities;

public class OrderApplication : IEntity, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid BuyerId { get; set; }
    public Guid BuyerTenantId { get; set; }
}

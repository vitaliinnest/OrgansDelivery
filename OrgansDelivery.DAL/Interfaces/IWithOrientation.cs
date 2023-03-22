using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.DAL.Interfaces;

public interface IWithOrientation
{
    OrientationLimits OrientationLimits { get; set; }
}

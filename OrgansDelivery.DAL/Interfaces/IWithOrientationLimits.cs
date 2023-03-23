using OrganStorage.DAL.Entities;

namespace OrganStorage.DAL.Interfaces;

public interface IWithOrientationLimits
{
    OrientationLimits OrientationLimits { get; set; }
}

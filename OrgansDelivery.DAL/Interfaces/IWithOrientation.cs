using OrganStorage.DAL.Entities;

namespace OrganStorage.DAL.Interfaces;

public interface IWithOrientation
{
    Orientation Orientation { get; set; }
}

using Microsoft.EntityFrameworkCore;

namespace OrganStorage.DAL.Entities;

public class Orientation
{
    // x & y rotation limits in degrees
    public decimal X { get; set; }
    public decimal Y { get; set; }
}

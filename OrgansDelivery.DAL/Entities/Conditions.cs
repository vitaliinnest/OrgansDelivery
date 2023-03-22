using OrgansDelivery.DAL.Interfaces;

namespace OrgansDelivery.DAL.Entities;

public class Conditions : IWithConditions, IWithOrientation
{
    // celsius
    public decimal Temperature { get; set; }
    // in percentage
    public decimal Humidity { get; set; }
    // lumens (lm)
    public decimal Light { get; set; }
    // x & y rotation in degrees
    public OrientationLimits OrientationLimits { get; set; }
}

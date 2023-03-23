using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Entities;

public class Conditions : IWithConditions, IWithOrientation
{
    // celsius
    public decimal Temperature { get; set; }
    // in percentage
    public decimal Humidity { get; set; }
    // lumens (lm)
    public decimal Light { get; set; }
    // X & Y
    public Orientation Orientation { get; set; }
}

using OrganStorage.DAL.Entities;

namespace OrganStorage.DAL.Interfaces;

public interface IWithExpectedConditions
{
    Condition<decimal> Humidity { get; set; }
    Condition<decimal> Light { get; set; }
    Condition<decimal> Temperature { get; set; }
    Condition<Orientation> Orientation { get; set; }
}

public class ExpectedConditions : IWithExpectedConditions
{
    public Condition<decimal> Humidity { get; set; }
    public Condition<decimal> Light { get; set; }
    public Condition<decimal> Temperature { get; set; }
    public Condition<Orientation> Orientation { get; set; }
}

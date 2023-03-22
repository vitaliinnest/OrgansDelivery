namespace OrgansDelivery.DAL.Interfaces;

public interface IWithConditions
{
    decimal Humidity { get; set; }
    decimal Light { get; set; }
    decimal Temperature { get; set; }
}

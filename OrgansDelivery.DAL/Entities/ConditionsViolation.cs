namespace OrganStorage.DAL.Entities;

public class ConditionsViolation
{
    public Guid ContainerId { get; set; }
    // todo: add endpoint to get record by id
    public Guid ConditionRecordId { get; set; }
    public ComparedResult<decimal> Temperature { get; set; }
    public ComparedResult<decimal> Humidity { get; set; }
    public ComparedResult<decimal> Light { get; set; }
    public ComparedResult<Orientation> Orientation { get; set; }
}

public class ComparedResult<T>
{
    public T Expected { get; set; }
    public T Actual { get; set; }
    public bool IsViolated { get; set; }
}

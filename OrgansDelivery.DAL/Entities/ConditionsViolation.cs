namespace OrganStorage.DAL.Entities;

public class ConditionsViolation
{
	public ComparedResult<decimal> Temperature { get; set; }
	public ComparedResult<decimal> Humidity { get; set; }
	public ComparedResult<decimal> Light { get; set; }
	public ComparedResult<Orientation> Orientation { get; set; }
	public ConditionsRecordRef Record { get; set; }

	public bool IsViolated() =>
		   Temperature.IsViolated
		|| Humidity.IsViolated
		|| Light.IsViolated
		|| Orientation.IsViolated;
}

public class ComparedResult<T> : Condition<T>
	where T : IEquatable<T>
{
	public T Actual { get; set; }
	public bool IsViolated { get; set; }
}

namespace NDifference.Reporting
{
    /// <summary>
    /// Describes a change that includes previous value and current value.
    /// </summary>
	public interface IDeltaDescriptor : IDescriptor
	{
		string Was { get; }

		string IsNow { get; }

        string Reason { get; }
    }
}

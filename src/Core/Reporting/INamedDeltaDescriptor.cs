namespace NDifference.Reporting
{
    /// <summary>
    /// Describes a change that includes a name, previous value and current value.
    /// </summary>
	public interface INamedDeltaDescriptor : IDeltaDescriptor
	{
		string Name { get; }
	}
}

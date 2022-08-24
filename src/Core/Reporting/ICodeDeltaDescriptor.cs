using NDifference.SourceFormatting;

namespace NDifference.Reporting
{
    /// <summary>
    /// Describes a code level change that includes previous value 
    /// and current value.
    /// </summary>
	public interface ICodeDeltaDescriptor : IDescriptor
    {
        ICoded Was { get; }

        ICoded IsNow { get; }
    }
}

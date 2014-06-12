
namespace NDifference.Analysis
{
	/// <summary>
	/// Represents a single change in an API.
	/// </summary>
	public sealed class IdentifiedChange
	{
		public IdentifiedChange()
		{
			this.Priority = CategoryPriority.InvalidValue;
			this.Inspector = "No Inspector Specified";
		}

		public string Description { get; set; }

		public int Priority { get; set; }

		public object Descriptor { get; set; }

		public string Inspector { get; set; }

		// public ISourceCode Code { get; set; }

		// public ISourceDelta Delta { get; set; }

		// descriptor object...
	}

}

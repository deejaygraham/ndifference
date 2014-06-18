
using NDifference.Inspectors;
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

		public IdentifiedChange(IInspector inspector, Category cat, string name)
		{
			this.Inspector = inspector.ShortCode;
			this.Priority = cat.Priority.Value;
			this.Description = name;
		}

		public IdentifiedChange(IInspector inspector, Category cat, object descriptor)
		{
			this.Inspector = inspector.ShortCode;
			this.Priority = cat.Priority.Value;
			this.Descriptor = descriptor;
		}

		public IdentifiedChange(IInspector inspector, Category cat, string name, object descriptor)
		{
			this.Inspector = inspector.ShortCode;
			this.Priority = cat.Priority.Value;
			this.Description = name;
			this.Descriptor = descriptor;
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


using NDifference.Inspectors;
using NDifference.Reporting;
using System.Collections.Generic;
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

	public class IdentifiedChangeComparer : IComparer<IdentifiedChange>
	{
		public int Compare(IdentifiedChange x, IdentifiedChange y)
		{
			if (x.Descriptor == null || y.Descriptor == null)
				return x.Description.CompareTo(y.Description);

			return 0;
		}

		private int CompareDescriptors(object x, object y)
		{
			IDocumentLink xlink = x as IDocumentLink;
			IDocumentLink ylink = y as IDocumentLink;

			if (xlink != null && ylink != null)
			{
				return xlink.LinkText.CompareTo(ylink.LinkText);
			}
			
			IDeltaDescriptor xdelta = x as IDeltaDescriptor;
			IDeltaDescriptor ydelta = y as IDeltaDescriptor;

			if (xdelta != null && ydelta != null)
			{
				return xdelta.Name.CompareTo(ydelta.Name);
			}

			ITextDescriptor xtext = x as ITextDescriptor;
			ITextDescriptor ytext = y as ITextDescriptor;

			if (xtext != null && ytext != null)
			{
				return xtext.Name.CompareTo(ytext.Name);
			}

			return 0;
		}
	}
}


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
			this.Category = cat;
		}

		public IdentifiedChange(IInspector inspector, Category cat, object descriptor)
		{
			this.Inspector = inspector.ShortCode;
			this.Priority = cat.Priority.Value;
			this.Descriptor = descriptor;
			this.Category = cat;
		}

		public IdentifiedChange(IInspector inspector, Category cat, string name, object descriptor)
		{
			this.Inspector = inspector.ShortCode;
			this.Priority = cat.Priority.Value;
			this.Description = name;
			this.Descriptor = descriptor;
			this.Category = cat;
		}

		public string Description { get; set; }

		public int Priority { get; set; }

		public object Descriptor { get; set; }

		public string Inspector { get; set; }

		public Category Category { get; set; }
	}

	public class IdentifiedChangeComparer : IComparer<IdentifiedChange>
	{
		public int Compare(IdentifiedChange x, IdentifiedChange y)
		{
			if (x.Descriptor == null || y.Descriptor == null)
				return x.Description.CompareTo(y.Description);

			return CompareDescriptors(x.Descriptor, y.Descriptor);
		}

		private int CompareDescriptors(object x, object y)
		{
			IDocumentLink xlink = x as IDocumentLink;
			IDocumentLink ylink = y as IDocumentLink;

			if (xlink != null && ylink != null)
			{
				return xlink.LinkText.CompareTo(ylink.LinkText);
			}

			ICodeDescriptor xcode = x as ICodeDescriptor;
			ICodeDescriptor ycode = y as ICodeDescriptor;

			if (xcode != null && ycode != null)
			{
				return xcode.Code.ToString().CompareTo(ycode.Code.ToString());
			}
	
			//IDeltaDescriptor xdelta = x as IDeltaDescriptor;
			//IDeltaDescriptor ydelta = y as IDeltaDescriptor;

			//if (xdelta != null && ydelta != null)
			//{
			//	return xdelta.Name.CompareTo(ydelta.Name);
			//}

			INameValueDescriptor xtext = x as INameValueDescriptor;
			INameValueDescriptor ytext = y as INameValueDescriptor;

			if (xtext != null && ytext != null)
			{
				return xtext.Name.CompareTo(ytext.Name);
			}

			return 0;
		}
	}
}

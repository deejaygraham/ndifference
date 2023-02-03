using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NDifference.Inspection;

namespace NDifference.Analysis
{
    /// <summary>
    /// Represents a single change in an API. Changes can be grouped into 
	/// collections which are ordered according to priority for reporting.
    /// </summary>
   // [DebuggerDisplay("{Description}")]
	public sealed class IdentifiedChange
	{
		public IdentifiedChange()
		{
			this.Priority = CategoryPriority.Uncategorised;
			this.Severity = Severity.Unknown;
		}

        public IdentifiedChange(/*IInspector inspector, */int categoryPriority, Severity severity, IDescriptor descriptor)
        {
            this.Priority = categoryPriority;
			this.Severity = severity;
            this.Descriptor = descriptor;

            bool debugAssignments = false;

            if (debugAssignments)
            {
                // TODO - Debug!!!
                var columns = WellKnownChangePriorities.ColumnNames(categoryPriority, false);
                int elementCount = descriptor.DataItemCount;

                if (columns.Count() != elementCount)
                {
                    Debug.Assert(false, string.Format("Row incorrectly formatted: Expecting {0} got {1}", columns.Count(), elementCount));
                }
            }
		}

		public Severity Severity { get; set; }

		//[Obsolete("Not used we don't think")]
        //public string Description { get; set; }

		public int Priority { get; set; }

		public IDescriptor Descriptor { get; set; }

		//[Obsolete("Not used we don't think")]
		//public string Inspector { get; set; }

        //		public Category Category { get; set; }

        //public AnalysisLevel Level { get; set; }

        public void ForType(ITypeInfo type)
        {
            this.TypeName = type.FullName;

            if (!String.IsNullOrEmpty(type.Assembly))
            {
                if (type.Assembly.EndsWith(".dll"))
                {
                    this.AssemblyName = type.Assembly;
                }
                else
                {
                    this.AssemblyName = type.Assembly + ".dll";
                }
            }
        }

        public void ForAssembly(string name)
        {
            this.AssemblyName = name;
        }

        public string AssemblyName { get; set; }

        public string TypeName { get; set; }
    }

	public class IdentifiedChangeComparer : IComparer<IdentifiedChange>
	{
		public int Compare(IdentifiedChange x, IdentifiedChange y)
		{
			//if (x.Descriptor == null || y.Descriptor == null)
			//	return x.Description.CompareTo(y.Description);

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

			ICodeSignature xcode = x as ICodeSignature;
            ICodeSignature ycode = y as ICodeSignature;

			if (xcode != null && ycode != null)
			{
				return xcode.Signature.ToString().CompareTo(ycode.Signature.ToString());
			}
	
			//IDeltaDescriptor xdelta = x as IDeltaDescriptor;
			//IDeltaDescriptor ydelta = y as IDeltaDescriptor;

			//if (xdelta != null && ydelta != null)
			//{
			//	return xdelta.Name.CompareTo(ydelta.Name);
			//}

			// TODO Change order ???
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

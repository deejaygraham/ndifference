using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;

namespace NDifference.Inspectors
{
	public class SourceOutputTypeInspector : ITypeInspector
	{
		public bool Enabled { get { return false; } set { } }

		public string ShortCode { get { return "TI00SEC"; } }

		public string DisplayName { get { return "Source Writer"; } }

		public string Description { get { return "Writes source code for each type found"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
        {
            var dummyChange = new IdentifiedChange(WellKnownChangePriorities.TypeDebug, 
				new CodeDeltaDescriptor 
				{ 
					Reason = "Debug code",
					Was = first.ToCode(), 
					IsNow = second.ToCode() 
				});

            dummyChange.ForType(first);

            changes.Add(dummyChange);
        }
	}
}

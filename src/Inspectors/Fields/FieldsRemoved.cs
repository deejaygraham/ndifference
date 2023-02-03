using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
	public class FieldsRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_FRI"; } }

		public string DisplayName { get { return "Removed Fields"; } }

		public string Description { get { return "Checks for existing fields removed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (firstClass.Fields.Any())
			{
				var removed = secondClass.Fields.FindRemovedMembers(firstClass.Fields);

				foreach (var rem in removed)
                {
                    var fieldRemoved = new IdentifiedChange(WellKnownChangePriorities.FieldsRemoved,
						Severity.BreakingChange,
						new RemovedSignature 
						{ 
							Signature = rem.ToCode() 
						});

					fieldRemoved.ForType(first);

                    changes.Add(fieldRemoved);
                }
			}
		}
	}

}

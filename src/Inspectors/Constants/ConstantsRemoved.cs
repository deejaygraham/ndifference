using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
	public class ConstantsRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CRI"; } }

		public string DisplayName { get { return "Removed Constants"; } }

		public string Description { get { return "Checks for existing constants removed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (firstClass.Constants.Any())
			{
				var removed = secondClass.Constants.FindRemovedMembers(firstClass.Constants);

				foreach (var rem in removed)
                {
                    var constantRemoved = new IdentifiedChange(WellKnownChangePriorities.ConstantsRemoved, 
						Severity.BreakingChange,
						new RemovedSignature 
						{ 
							Reason = "Constant removed",
							Signature = rem.ToCode() 
						});

					constantRemoved.ForType(first);

                    changes.Add(constantRemoved);
                }
			}
		}
	}
}

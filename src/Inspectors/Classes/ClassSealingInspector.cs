using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Diagnostics;

namespace NDifference.Inspectors
{
    public class ClassSealingInspector : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CSI"; } }

		public string DisplayName { get { return "Class Sealing"; } }

		public string Description { get { return "Checks for classes that are sealed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (!firstClass.IsSealed && secondClass.IsSealed)
            {
                var classNowSealed = new IdentifiedChange(WellKnownChangePriorities.TypeInternal,
					Severity.BreakingChange,
					new ChangedCodeSignature
					{ 
                        Reason = "Class is now marked as sealed", 
						Was = first.ToCode(), 
						IsNow = second.ToCode() 
					});

				classNowSealed.ForType(first);

                changes.Add(classNowSealed);
            }
		}
	}
}

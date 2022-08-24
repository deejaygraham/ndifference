using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Diagnostics;

namespace NDifference.Inspectors
{
    public class ClassAbstractionInspector : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CAI"; } }

		public string DisplayName { get { return "Class Abstraction"; } }

		public string Description { get { return "Checks for classes that are now abstract"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (!firstClass.IsAbstract && secondClass.IsAbstract)
            {
                var classMadeAbstract = new IdentifiedChange(WellKnownChangePriorities.TypeInternal, 
                    new CodeDeltaDescriptor
					{ 
                        Reason = "Class is now abstract", 
                        Was = first.ToCode(), 
                        IsNow = second.ToCode() 
                    });

				classMadeAbstract.ForType(first);

                changes.Add(classMadeAbstract);
            }
		}
	}
}

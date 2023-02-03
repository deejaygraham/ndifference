using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;

namespace NDifference.Inspectors
{
	public class StaticConstructorRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_C006"; } }

		public string DisplayName { get { return "Remove Static Constructor"; } }

		public string Description { get { return "Looks for static constructor removed."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Class)
			{
				ClassDefinition cd1 = first as ClassDefinition;
				ClassDefinition cd2 = second as ClassDefinition;

				StaticConstructor oldStatic = cd1.StaticConstructor;
				StaticConstructor newStatic = cd2.StaticConstructor;

				if (oldStatic != null && newStatic == null)
                {
                    var constructorRemoved = new IdentifiedChange(WellKnownChangePriorities.ConstructorsRemoved,
						Severity.BreakingChange,
						new RemovedSignature
						{ 
							Reason = "Static constructor removed",
							Signature = oldStatic.ToCode() 
						});

					constructorRemoved.ForType(first);

                    changes.Add(constructorRemoved);
                }
			}
		}
	}
}

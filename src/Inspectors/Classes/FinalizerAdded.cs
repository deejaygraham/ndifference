using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;

namespace NDifference.Inspectors
{
    public class FinalizerAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI006"; } }

		public string DisplayName { get { return "New Finalizer"; } }

		public string Description { get { return "Looks for new finalizer for a type."; } }
		
		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				&& second.Taxonomy == TypeTaxonomy.Class)
			{
				ClassDefinition cd1 = first as ClassDefinition;
				ClassDefinition cd2 = second as ClassDefinition;

				Finalizer wasDestructor = cd1.Finalizer;
				Finalizer nowDestructor = cd2.Finalizer;

				if (wasDestructor == null && nowDestructor != null)
                {
                    var finalizerAdded = new IdentifiedChange(WellKnownChangePriorities.FinalizersAdded,
						Severity.NonBreaking,
						new CodeDescriptor 
						{ 
							Code = nowDestructor.ToCode(),
							Reason = "Finalizer added"
						});

					finalizerAdded.ForType(first);

                    changes.Add(finalizerAdded);
                }
			}
		}
	}
}

using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;

namespace NDifference.Inspectors
{
	public class FinalizerRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI007"; } }

		public string DisplayName { get { return "Removed Finalizer"; } }

		public string Description { get { return "Looks for removed finalizer from a type."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				&& second.Taxonomy == TypeTaxonomy.Class)
			{
				ClassDefinition cd1 = first as ClassDefinition;
				ClassDefinition cd2 = second as ClassDefinition;

				Finalizer wasDestructor = cd1.Finalizer;
				Finalizer nowDestructor = cd2.Finalizer;

				if (wasDestructor != null && nowDestructor == null)
                {
                    var finalizerRemoved = new IdentifiedChange(WellKnownChangePriorities.FinalizersRemoved, 
						new CodeDescriptor 
						{ 
							Reason = "Finalizer removed",
							Code = wasDestructor.ToCode() 
						});

					finalizerRemoved.ForType(first);

                    changes.Add(finalizerRemoved);
                }
			}
		}
	}

}

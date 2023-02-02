using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.SourceFormatting;
using NDifference.TypeSystem;
using System.Linq;

namespace NDifference.Inspectors
{
    /// <summary>
    /// Looking for changes in the interfaces a type implements.
    /// </summary>
    public class InterfacesRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		// need to validate all short codes...
		public string ShortCode { get { return "TI005"; } }

		public string DisplayName { get { return "Removed Interfaces"; } }

		public string Description { get { return "Looks for interfaces removed from a type."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				IReferenceTypeDefinition ref1 = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition ref2 = second as IReferenceTypeDefinition;

				var removed = ref1.Implements.RemovedFrom(ref2.Implements);

				if (removed.Any())
				{
					// or for each - no longer implements...
					foreach (var remove in removed)
                    {
                        var interfaceRemoved = new IdentifiedChange(WellKnownChangePriorities.TypeInternal,
							Severity.BreakingChange,
							new CodeDeltaDescriptor 
							{ 
								Reason = "No longer implements", 
								Was = remove.ToCode(),
								IsNow = SourceCode.NoOp
							});

						interfaceRemoved.ForType(first);

                        changes.Add(interfaceRemoved);
                    }
				}
			}
		}
	}

}

using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Linq;

namespace NDifference.Inspectors
{
    public class MethodsAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_MAI"; } }

		public string DisplayName { get { return "New Methods"; } }

		public string Description { get { return "Checks for new methods added in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				if (secondRef.AllMethods.Any())
				{
					var added = secondRef.AllMethods.FindAddedMembers(firstRef.AllMethods);

					foreach (var add in added)
                    {
                        var newMethodAdded = new IdentifiedChange(WellKnownChangePriorities.MethodsAdded, 
							new CodeDescriptor 
							{ 
								Reason = "Method added",
								Code = add.ToCode() 
							});
                        
                        newMethodAdded.ForType(first);

                        changes.Add(newMethodAdded);
                    }
				}
			}
		}
	}
}

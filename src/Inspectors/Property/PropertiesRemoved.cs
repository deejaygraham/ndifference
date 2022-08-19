using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Linq;

namespace NDifference.Inspectors
{
	public class PropertiesRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_PRI"; } }

		public string DisplayName { get { return "Removed Properties"; } }

		public string Description { get { return "Checks for existing properties removed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				if (firstRef.AllProperties.Any())
				{
					var removed = secondRef.Properties(MemberVisibilityOption.Public).FindRemovedMembers(firstRef.Properties(MemberVisibilityOption.Public));

					foreach (var rem in removed)
                    {
                        var removedProperty = new IdentifiedChange(WellKnownChangePriorities.PropertiesRemoved, new CodeDescriptor { Code = rem.ToCode() });
                        
                        removedProperty.ForType(first);

                        changes.Add(removedProperty);
                    }
				}
			}
		}
	}

}

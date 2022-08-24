using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Linq;

namespace NDifference.Inspectors
{
	public class PropertiesAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_PAI"; } }

		public string DisplayName { get { return "New Properties"; } }

		public string Description { get { return "Checks for new properties added in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				if (secondRef.AllProperties.Any())
				{
					var added = secondRef.Properties(MemberVisibilityOption.Public).FindAddedMembers(firstRef.Properties(MemberVisibilityOption.Public));

					foreach (var add in added)
                    {
                        var propertyAdded = new IdentifiedChange(WellKnownChangePriorities.PropertiesAdded, 
							new CodeDescriptor 
							{ 
								Code = add.ToCode(),
								Reason = "Property has been added"
							});
                        
                        propertyAdded.ForType(first);

                        changes.Add(propertyAdded);
                    }
				}
			}
		}
	}

}

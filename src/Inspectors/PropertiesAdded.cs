using NDifference.Analysis;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class PropertiesAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_PAI"; } }

		public string DisplayName { get { return "Added Properties"; } }

		public string Description { get { return "Checks for new properties added in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{

				changes.Add(WellKnownTypeCategories.PropertiesAdded);

				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				if (secondRef.Properties.Any())
				{
					var removed = secondRef.Properties.FindAddedMembers(firstRef.Properties);

					foreach (var rem in removed)
					{
						changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.PropertiesAdded, new TextDescriptor { Name = rem.ToString(), Message = rem.ToCode() }));
					}
				}
			}
		}
	}

}

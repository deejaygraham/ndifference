using NDifference.Analysis;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

				changes.Add(WellKnownTypeCategories.PropertiesRemoved);

				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				if (firstRef.Properties.Any())
				{
					var removed = secondRef.Properties.FindRemovedMembers(firstRef.Properties);

					foreach (var rem in removed)
					{
						changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.PropertiesRemoved, new CodeDescriptor { Code = rem.ToCode() }));
					}
				}
			}
		}
	}

}

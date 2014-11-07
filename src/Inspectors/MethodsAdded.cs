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
				changes.Add(WellKnownTypeCategories.MethodsAdded);

				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				if (secondRef.Methods.Any())
				{
					var added = secondRef.Methods.FindAddedMembers(firstRef.Methods);

					foreach (var add in added)
					{
						changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsAdded, new CodeDescriptor { Code = add.ToCode() }));
					}
				}
			}
		}
	}

}

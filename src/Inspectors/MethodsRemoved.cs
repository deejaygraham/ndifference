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
	public class MethodsRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_MRI"; } }

		public string DisplayName { get { return "Removed Methods"; } }

		public string Description { get { return "Checks for existing methods removed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				changes.Add(WellKnownTypeCategories.MethodsRemoved);

				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				if (firstRef.Methods.Any())
				{
					var removed = secondRef.Methods.FindRemovedMembers(firstRef.Methods);

					foreach (var rem in removed)
					{
						changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsRemoved, new TextDescriptor { Name = rem.ToString(), Message = rem.ToCode() }));
					}
				}
			}
		}
	}

}

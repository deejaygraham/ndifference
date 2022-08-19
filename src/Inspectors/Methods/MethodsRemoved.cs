using NDifference.Analysis;
using NDifference.Inspection;
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
				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				if (firstRef.AllMethods.Any())
				{
					var removed = secondRef.Methods(MemberVisibilityOption.Public).FindRemovedMembers(firstRef.Methods(MemberVisibilityOption.Public));

					foreach (var rem in removed)
                    {
                        var methodRemoved = new IdentifiedChange(WellKnownChangePriorities.MethodsRemoved, new CodeDescriptor { Code = rem.ToCode() });
						
                        methodRemoved.ForType(first);

                        changes.Add(methodRemoved);
                    }
				}
			}
		}
	}

}

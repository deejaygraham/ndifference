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
	public class MethodsObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_MOI"; } }

		public string DisplayName { get { return "Obsolete Methods"; } }

		public string Description { get { return "Checks for obsolete methods"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				changes.Add(WellKnownTypeCategories.MethodsObsolete);

				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				var obs = secondRef.Methods.FindObsoleteMembers();

				foreach (var o in obs)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsObsolete, new TextDescriptor { Name = o.ToString(), Message = o.ObsoleteMarker.Message }));
				}
			}
		}
	}
}

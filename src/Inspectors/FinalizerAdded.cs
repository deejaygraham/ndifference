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
	public class FinalizerAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI006"; } }

		public string DisplayName { get { return "New Finalizer"; } }

		public string Description { get { return "Looks for new finalizer for a type."; } }
		
		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			changes.Add(WellKnownTypeCategories.FinalizersAdded);

			if (first.Taxonomy == TypeTaxonomy.Class
				&& second.Taxonomy == TypeTaxonomy.Class)
			{
				ClassDefinition cd1 = first as ClassDefinition;
				ClassDefinition cd2 = second as ClassDefinition;

				Finalizer wasDestructor = cd1.Finalizer;
				Finalizer nowDestructor = cd2.Finalizer;

				if (wasDestructor == null && nowDestructor != null)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.FinalizersAdded, new TextDescriptor { Name = "Finalizer added", Message = nowDestructor.ToCode() }));
				}
			}
		}
	}
}

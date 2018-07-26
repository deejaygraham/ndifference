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
	public class StaticConstructorRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_C006"; } }

		public string DisplayName { get { return "Remove Static Constructor"; } }

		public string Description { get { return "Looks for static constructor removed."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Class)
			{
				ClassDefinition cd1 = first as ClassDefinition;
				ClassDefinition cd2 = second as ClassDefinition;

				StaticConstructor oldStatic = cd1.StaticConstructor;
				StaticConstructor newStatic = cd2.StaticConstructor;

				if (oldStatic != null && newStatic == null)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.ConstructorsRemoved, new ValueDescriptor { Value = oldStatic.ToCode() }));
				}
			}
		}
	}
}

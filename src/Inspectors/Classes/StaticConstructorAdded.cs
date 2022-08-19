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
	public class StaticConstructorAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_D006"; } }

		public string DisplayName { get { return "New Static Constructor"; } }

		public string Description { get { return "Looks for new static constructor."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Class)
			{
				ClassDefinition cd1 = first as ClassDefinition;
				ClassDefinition cd2 = second as ClassDefinition;

				StaticConstructor oldStatic = cd1.StaticConstructor;
				StaticConstructor newStatic = cd2.StaticConstructor;

				if (oldStatic == null && newStatic != null)
                {
                    var addedConstructor = new IdentifiedChange(WellKnownChangePriorities.ConstructorsAdded, new CodeDescriptor { Code = newStatic.ToCode() });

					addedConstructor.ForType(first);

                    changes.Add(addedConstructor);
                }
			}
		}
	}
}

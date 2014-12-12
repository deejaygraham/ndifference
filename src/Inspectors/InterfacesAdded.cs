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
	/// <summary>
	/// Looking for changes in the interfaces a type implements.
	/// </summary>
	public class InterfacesAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI004"; } }

		public string DisplayName { get { return "New Interfaces"; } }

		public string Description { get { return "Looks for new interfaces added to a type."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			// TODO new category - hierarchy changes ???
			changes.Add(WellKnownTypeCategories.TypeInternal);

			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				IReferenceTypeDefinition ref1 = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition ref2 = second as IReferenceTypeDefinition;

				var added = ref1.Implements.AddedTo(ref2.Implements);

				if (added.Any())
				{
					foreach (var add in added)
					{
						changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.TypeInternal, new NamedDeltaDescriptor { Name = "Now implements", Was = string.Empty, IsNow = add.ToCode() }));
					}
				}
			}
		}
	}
}

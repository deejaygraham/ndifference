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
	public class EventsRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_ERI"; } }

		public string DisplayName { get { return "Removed Events"; } }

		public string Description { get { return "Checks for existing events removed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			changes.Add(WellKnownTypeCategories.EventsRemoved);

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			if (firstClass.Events.Any())
			{
				var removed = secondClass.Events.FindRemovedMembers(firstClass.Events);

				foreach (var rem in removed)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.EventsRemoved, new CodeDescriptor { Code = rem.ToCode() }));
				}
			}
		}
	}

}

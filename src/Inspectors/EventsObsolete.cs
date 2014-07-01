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
	public class EventsObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_EOI"; } }

		public string DisplayName { get { return "Obsolete Events"; } }

		public string Description { get { return "Checks for obsolete events"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			changes.Add(WellKnownTypeCategories.EventsObsolete);

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			var obs = secondClass.Events.FindObsoleteMembers();

			foreach (var o in obs)
			{
				changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.EventsObsolete, new TextDescriptor { Name = o.ToString(), Message = o.ObsoleteMarker.Message }));
			}
		}
	}
}

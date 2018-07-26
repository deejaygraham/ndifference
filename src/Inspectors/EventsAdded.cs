using NDifference.Analysis;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Linq;

namespace NDifference.Inspectors
{
	public class EventsAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_EAI"; } }

		public string DisplayName { get { return "New Events"; } }

		public string Description { get { return "Checks for new events added in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			if (secondClass.Events.Any())
			{
				var added = secondClass.Events.FindAddedMembers(firstClass.Events);

				foreach (var add in added)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.EventsAdded, new CodeDescriptor { Code = add.ToCode() }));
				}
			}
		}
	}

}

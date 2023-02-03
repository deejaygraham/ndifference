using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Linq;

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

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			if (firstClass.Events.Any())
			{
				var removed = secondClass.Events.FindRemovedMembers(firstClass.Events);

				foreach (var rem in removed)
                {
                    var fieldRemoved = new IdentifiedChange(WellKnownChangePriorities.EventsRemoved,
						Severity.BreakingChange,
						new RemovedSignature 
						{ 
							Reason = "Event removed",
							Signature = rem.ToCode() 
						});

					fieldRemoved.ForType(first);

                    changes.Add(fieldRemoved);
                }
			}
		}
	}

}

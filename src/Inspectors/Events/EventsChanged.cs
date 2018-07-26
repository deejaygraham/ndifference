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
	public class EventsChanged : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CE"; } }

		public string DisplayName { get { return "Changed Events"; } }

		public string Description { get { return "Checks for changes to existing events"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			if (firstClass.Events.Any() || secondClass.Events.Any())
			{
				var commonEvents = firstClass.Events.FuzzyInCommonWith(secondClass.Events);

				foreach (var commonEvent in commonEvents)
				{
					MemberEvent oldEvent = commonEvent.Item1;
					MemberEvent newEvent = commonEvent.Item2;

					if (oldEvent != null && newEvent != null)
					{
						if (oldEvent.EventType != newEvent.EventType)
						{
							changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.EventsChanged,
								new NamedDeltaDescriptor
								{
									Name = string.Format("Changed type from {0} to {1}", oldEvent.EventType, newEvent.EventType),
									Was = oldEvent.ToCode(),
									IsNow = newEvent.ToCode()
								}));
						}
					}
				}
			}
		}
	}

}

using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NDifference.Inspectors
{
	public class EventsObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_EOI"; } }

		public string DisplayName { get { return "Obsolete Events - Nag Mode"; } }

		public string Description { get { return "Checks for obsolete events"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			//ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			var obs = secondClass.Events.FindObsoleteMembers();

			foreach (var o in obs)
            {
                var eventMadeObsolete = new IdentifiedChange(WellKnownChangePriorities.EventsObsolete,
                    Severity.BreakingChange,
                    new ObsoleteSignature
                    { 
                        Signature = o.ToCode(), 
                        Reason = o.ObsoleteMarker.Message 
                    });  

                eventMadeObsolete.ForType(first);

                changes.Add(eventMadeObsolete);
            }
		}
	}

    // TODO - Common checks

    public class EventsNowObsolete : ITypeInspector
    {
        public bool Enabled { get; set; }

        public string ShortCode { get { return "TI_EOI - 2"; } }

        public string DisplayName { get { return "Obsolete Events - New"; } }

        public string Description { get { return "Checks for obsolete events"; } }

        public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
        {
            if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
                return;

            ClassDefinition firstClass = first as ClassDefinition;
            var oldObs = firstClass.Events.FindObsoleteMembers();

            ClassDefinition secondClass = second as ClassDefinition;
            var newObs = secondClass.Events.FindObsoleteMembers();

            foreach (var o in newObs.Except(oldObs, new CompareMemberEventByName()))
            {
                var eventMadeObsolete = new IdentifiedChange(WellKnownChangePriorities.EventsObsolete,
                    Severity.BreakingChange,
                    new ObsoleteSignature
                    { 
                        Signature = o.ToCode(), 
                        Reason = o.ObsoleteMarker.Message 
                    });

                eventMadeObsolete.ForType(first);

                changes.Add(eventMadeObsolete);
            }
        }
    }

    internal sealed class CompareMemberEventByName : IEqualityComparer<MemberEvent>
    {
        public bool Equals(MemberEvent x, MemberEvent y)
        {
            const int ExactMatch = 0;
            return string.Compare(
                                x.Name,
                                y.Name,
                                StringComparison.OrdinalIgnoreCase)
                                == ExactMatch;
        }

        public int GetHashCode(MemberEvent obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

}

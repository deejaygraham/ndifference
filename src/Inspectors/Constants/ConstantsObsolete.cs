using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
	public class ConstantsObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_COI"; } }

		public string DisplayName { get { return "Obsolete Constants - Nag Mode"; } }

		public string Description { get { return "Checks for obsolete constants"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
            var inspector = new InspectObsoleteConstants
            {
                NagMode = true
            };

            inspector.Inspect(this, first, second, changes);
        }
	}

    public class ConstantsNowObsolete : ITypeInspector
    {
        public bool Enabled { get; set; }

        public string ShortCode { get { return "TI_COI - 2"; } }

        public string DisplayName { get { return "Obsolete Constants - New"; } }

        public string Description { get { return "Checks for obsolete constants"; } }

        public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
        {
            var inspector = new InspectObsoleteConstants
            {
                NagMode = false
            };

            inspector.Inspect(this, first, second, changes);
        }
    }

    class InspectObsoleteConstants
    {
        public bool NagMode { get; set; }

        public void Inspect(ITypeInspector parentInspector, ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
        {
            if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
                return;

            IEnumerable<Constant> obsoleteConstants = null;

            if (this.NagMode)
            {
                ClassDefinition secondClass = second as ClassDefinition;
                Debug.Assert(secondClass != null, "Second type is not a class");

                obsoleteConstants = secondClass.Constants.FindObsoleteMembers();
            }
            else
            {
                ClassDefinition firstClass = first as ClassDefinition;
                Debug.Assert(firstClass != null, "First type is not a class");
                var oldObs = firstClass.Constants.FindObsoleteMembers();

                ClassDefinition secondClass = second as ClassDefinition;
                Debug.Assert(secondClass != null, "Second type is not a class");
                var newObs = secondClass.Constants.FindObsoleteMembers();

                obsoleteConstants = newObs.Except(oldObs, new CompareMemberConstantByName());
            }

            foreach (var o in obsoleteConstants)
            {
                var constantMadeObsolete = new IdentifiedChange(WellKnownChangePriorities.ConstantsObsolete,
                    this.NagMode ? Severity.LegacyBreakingChange : Severity.BreakingChange,
                    new ObsoleteSignature
                    { 
                        Signature = o.ToCode(), 
                        Reason = o.ObsoleteMarker.Message 
                    });

                constantMadeObsolete.ForType(first);

                changes.Add(constantMadeObsolete);
            }
        }
    }

    internal sealed class CompareMemberConstantByName : IEqualityComparer<Constant>
    {
        public bool Equals(Constant x, Constant y)
        {
            const int ExactMatch = 0;
            return string.Compare(
                                x.Name,
                                y.Name,
                                StringComparison.OrdinalIgnoreCase)
                                == ExactMatch;
        }

        public int GetHashCode(Constant obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

}

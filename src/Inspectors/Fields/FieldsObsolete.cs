using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class FieldsObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_FOI"; } }

		public string DisplayName { get { return "Obsolete Fields - Nag Mode"; } }

		public string Description { get { return "Checks for obsolete fields"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			//ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			//Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			var obs = secondClass.Fields.FindObsoleteMembers();

			foreach (var o in obs)
            {
                var fieldMadeObsolete = new IdentifiedChange(WellKnownChangePriorities.FieldsObsolete,
                    Severity.BreakingChange,
                    new ObsoleteSignature
                    { 
                        Signature = o.ToCode(), 
                        Reason = o.ObsoleteMarker.Message 
                    });

                fieldMadeObsolete.ForType(first);

                changes.Add(fieldMadeObsolete);
            }
		}
	}

    public class FieldsNowObsolete : ITypeInspector
    {
        public bool Enabled { get; set; }

        public string ShortCode { get { return "TI_FOI - 2"; } }

        public string DisplayName { get { return "Obsolete Fields - New"; } }

        public string Description { get { return "Checks for obsolete fields"; } }

        public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
        {
            if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
                return;

            ClassDefinition firstClass = first as ClassDefinition;
            Debug.Assert(firstClass != null, "First type is not a class");
            var oldObs = firstClass.Fields.FindObsoleteMembers();

            ClassDefinition secondClass = second as ClassDefinition;
            Debug.Assert(secondClass != null, "Second type is not a class");

            var newObs = secondClass.Fields.FindObsoleteMembers();

            foreach (var o in newObs.Except(oldObs, new CompareMemberFieldByName()))
            {
                var fieldMadeObsolete = new IdentifiedChange(WellKnownChangePriorities.FieldsObsolete,
                    Severity.BreakingChange,
                    new ObsoleteSignature
                    { 
                        Signature = o.ToCode(), 
                        Reason = o.ObsoleteMarker.Message 
                    });

                fieldMadeObsolete.ForType(first);

                changes.Add(fieldMadeObsolete);
            }
        }
    }


    internal sealed class CompareMemberFieldByName : IEqualityComparer<MemberField>
    {
        public bool Equals(MemberField x, MemberField y)
        {
            const int ExactMatch = 0;
            return string.Compare(
                                x.Name,
                                y.Name,
                                StringComparison.OrdinalIgnoreCase)
                                == ExactMatch;
        }

        public int GetHashCode(MemberField obj)
        {
            return obj.ToString().GetHashCode();
        }
    }


}

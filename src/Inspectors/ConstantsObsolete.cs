using NDifference.Analysis;
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
	public class ConstantsObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_COI"; } }

		public string DisplayName { get { return "Obsolete Constants - Nag Mode"; } }

		public string Description { get { return "Checks for obsolete constants"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			var obs = secondClass.Constants.FindObsoleteMembers();

			foreach(var o in obs)
			{
				changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.ConstantsObsolete, new NameValueDescriptor { Name = o.ToString(), Value = o.ObsoleteMarker.Message }));
			}
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
            if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
                return;

            ClassDefinition firstClass = first as ClassDefinition;
            Debug.Assert(firstClass != null, "First type is not a class");
            var oldObs = firstClass.Constants.FindObsoleteMembers();

            ClassDefinition secondClass = second as ClassDefinition;
            Debug.Assert(secondClass != null, "Second type is not a class");
            var newObs = secondClass.Constants.FindObsoleteMembers();

            foreach (var o in newObs.Except(oldObs, new CompareMemberConstantByName()))
            {
                changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.ConstantsObsolete, new NameValueDescriptor { Name = o.ToString(), Value = o.ObsoleteMarker.Message }));
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

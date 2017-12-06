using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class PropertiesObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_POI"; } }

		public string DisplayName { get { return "Obsolete Properties - Nag Mode"; } }

		public string Description { get { return "Checks for obsolete properties"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{

				changes.Add(WellKnownTypeCategories.PropertiesObsolete);

				//IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				var obs = secondRef.Properties(MemberVisibilityOption.Public).FindObsoleteMembers();

				foreach (var o in obs)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.PropertiesObsolete, new NameValueDescriptor { Name = o.ToString(), Value = o.ObsoleteMarker.Message }));
				}
			}

		}
	}

    public class PropertiesNowObsolete : ITypeInspector
    {
        public bool Enabled { get; set; }

        public string ShortCode { get { return "TI_POI - 2"; } }

        public string DisplayName { get { return "Obsolete Properties - New"; } }

        public string Description { get { return "Checks for obsolete properties"; } }

        public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
        {
            if (first.Taxonomy == TypeTaxonomy.Class
                || first.Taxonomy == TypeTaxonomy.Interface
                || second.Taxonomy == TypeTaxonomy.Class
                || second.Taxonomy == TypeTaxonomy.Interface)
            {

                changes.Add(WellKnownTypeCategories.PropertiesObsolete);

                IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
                var oldObs = firstRef.Properties(MemberVisibilityOption.Public).FindObsoleteMembers();

                IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;
                var newObs = secondRef.Properties(MemberVisibilityOption.Public).FindObsoleteMembers();

                foreach (var o in newObs.Except(oldObs, new CompareMemberPropertyByName()))
                {
                    changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.PropertiesObsolete, new NameValueDescriptor { Name = o.ToString(), Value = o.ObsoleteMarker.Message }));
                }
            }

        }
    }


    internal sealed class CompareMemberPropertyByName : IEqualityComparer<MemberProperty>
    {
        public bool Equals(MemberProperty x, MemberProperty y)
        {
            const int ExactMatch = 0;
            return string.Compare(
                                x.Name,
                                y.Name,
                                StringComparison.OrdinalIgnoreCase)
                                == ExactMatch;
        }

        public int GetHashCode(MemberProperty obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}

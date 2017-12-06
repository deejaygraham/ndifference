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
	public class MethodsObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_MOI"; } }

		public string DisplayName { get { return "Obsolete Methods - Nag Mode"; } }

		public string Description { get { return "Checks for obsolete methods"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				changes.Add(WellKnownTypeCategories.MethodsObsolete);

				//IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				var obs = secondRef.Methods(MemberVisibilityOption.Public).FindObsoleteMembers();

				foreach (var o in obs)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsObsolete, new NameValueDescriptor { Name = o.ToString(), Value = o.ObsoleteMarker.Message }));
				}
			}
		}
	}

    public class MethodsNowObsolete : ITypeInspector
    {
        public bool Enabled { get; set; }

        public string ShortCode { get { return "TI_MOI - 2"; } }

        public string DisplayName { get { return "Obsolete Methods - New"; } }

        public string Description { get { return "Checks for obsolete methods"; } }

        public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
        {
            if (first.Taxonomy == TypeTaxonomy.Class
                || first.Taxonomy == TypeTaxonomy.Interface
                || second.Taxonomy == TypeTaxonomy.Class
                || second.Taxonomy == TypeTaxonomy.Interface)
            {
                changes.Add(WellKnownTypeCategories.MethodsObsolete);

                IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
                var oldObs = firstRef.Methods(MemberVisibilityOption.Public).FindObsoleteMembers();

                IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;
                var newObs = secondRef.Methods(MemberVisibilityOption.Public).FindObsoleteMembers();

                foreach (var o in newObs.Except(oldObs, new CompareMemberMethodByName()))
                {
                    changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsObsolete, new NameValueDescriptor { Name = o.ToString(), Value = o.ObsoleteMarker.Message }));
                }
            }
        }
    }

    internal sealed class CompareMemberMethodByName : IEqualityComparer<IMemberMethod>
    {
        public bool Equals(IMemberMethod x, IMemberMethod y)
        {
            const int ExactMatch = 0;
            return string.Compare(
                                x.Signature.ToString(),
                                y.Signature.ToString(),
                                StringComparison.OrdinalIgnoreCase)
                                == ExactMatch;
        }

        public int GetHashCode(IMemberMethod obj)
        {
            return obj.ToString().GetHashCode();
        }
    }


}

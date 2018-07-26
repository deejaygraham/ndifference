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
	public class InstanceConstructorsObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CC006"; } }

		public string DisplayName { get { return "Instance Constructors Obsolete - Nag Mode"; } }

		public string Description { get { return "Looks for obsolete constructors."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Class)
			{
                ClassDefinition cd2 = second as ClassDefinition;
				var obs = cd2.Constructors.FindObsoleteMembers();

				foreach (var o in obs)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.ConstructorsObsolete, new NameValueDescriptor { Name = o.ToString(), Value = o.ObsoleteMarker.Message }));
				}
			}
		}
	}

    public class InstanceConstructorsNowObsolete : ITypeInspector
    {
        public bool Enabled { get; set; }

        public string ShortCode { get { return "TI_CC006 - 2"; } }

        public string DisplayName { get { return "Instance Constructors Obsolete - New"; } }

        public string Description { get { return "Looks for obsolete constructors."; } }

        public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
        {
            if (first.Taxonomy == TypeTaxonomy.Class
                || second.Taxonomy == TypeTaxonomy.Class)
            {
                ClassDefinition cd1 = first as ClassDefinition;
                var oldObs = cd1.Constructors.FindObsoleteMembers();

                ClassDefinition cd2 = second as ClassDefinition;
                var newObs = cd2.Constructors.FindObsoleteMembers();

                foreach (var o in newObs.Except(oldObs, new CompareMemberMethodByName()))
                {
                    changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.ConstructorsObsolete, new NameValueDescriptor { Name = o.ToString(), Value = o.ObsoleteMarker.Message }));
                }
            }
        }
    }

}

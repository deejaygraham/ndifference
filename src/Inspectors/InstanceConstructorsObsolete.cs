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

		public string DisplayName { get { return "Instance Constructors Obsolete"; } }

		public string Description { get { return "Looks for obsolete constructors."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Class)
			{

				changes.Add(WellKnownTypeCategories.ConstructorsObsolete);
				
				ClassDefinition cd1 = first as ClassDefinition;
				ClassDefinition cd2 = second as ClassDefinition;

				var obs = cd2.Constructors.FindObsoleteMembers();

				foreach (var o in obs)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.ConstructorsObsolete, new TextDescriptor { Name = o.ToString(), Message = o.ObsoleteMarker.Message }));
				}
			}
		}
	}
}

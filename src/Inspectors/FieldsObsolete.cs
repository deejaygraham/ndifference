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
	public class FieldsObsolete : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_FOI"; } }

		public string DisplayName { get { return "Obsolete Fields"; } }

		public string Description { get { return "Checks for obsolete fields"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			changes.Add(WellKnownTypeCategories.FieldsObsolete);

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			var obs = secondClass.Fields.FindObsoleteMembers();

			foreach (var o in obs)
			{
				changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.FieldsObsolete, new TextDescriptor { Name = o.ToString(), Message = o.ObsoleteMarker.Message }));
			}
		}
	}

}

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
	public class FieldsRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_FRI"; } }

		public string DisplayName { get { return "Removed Fields"; } }

		public string Description { get { return "Checks for existing fields removed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			changes.Add(WellKnownTypeCategories.FieldsRemoved);

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (firstClass.Fields.Any())
			{
				var removed = secondClass.Fields.FindRemovedMembers(firstClass.Fields);

				foreach (var rem in removed)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.FieldsRemoved, new TextDescriptor { Name = rem.ToString(), Message = rem.ToCode() }));
				}
			}
		}
	}

}

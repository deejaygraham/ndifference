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

	public class FieldsAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_FAI"; } }

		public string DisplayName { get { return "Added Fields"; } }

		public string Description { get { return "Checks for new fields added in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			changes.Add(WellKnownTypeCategories.FieldsAdded);

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (secondClass.Fields.Any())
			{
				var added = secondClass.Fields.FindAddedMembers(firstClass.Fields);

				foreach (var add in added)
				{
					changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.FieldsAdded, new CodeDescriptor { Code = add.ToCode() }));
				}
			}
		}
	}

}

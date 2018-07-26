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
	public class ClassSealingInspector : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CSI"; } }

		public string DisplayName { get { return "Class Sealing"; } }

		public string Description { get { return "Checks for classes that are sealed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (!firstClass.IsSealed && secondClass.IsSealed)
			{
				changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.TypeInternal, new NamedDeltaDescriptor { Name = "Class is now marked as sealed", Was = first.ToCode(), IsNow = second.ToCode() }));
			}
		}
	}
}

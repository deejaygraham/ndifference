using NDifference.Analysis;
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

		public string ShortCode { get { return "TI001"; } }

		public string DisplayName { get { return "Class Sealing"; } }

		public string Description { get { return "Checks for classes that are sealed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(first != null, "First type is not a class");
			Debug.Assert(second != null, "Second type is not a class");

			if (!firstClass.IsSealed && secondClass.IsSealed)
			{
				changes.Add(new IdentifiedChange
				{
					Description = "Class is now marked as sealed",
					Priority = 1,// need value... for type taxonomy-like changes,
					Inspector = this.ShortCode

				});
			}
		}
	}
}

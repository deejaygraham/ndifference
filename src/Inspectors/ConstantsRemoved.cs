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
	public class ConstantsRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CRI"; } }

		public string DisplayName { get { return "Removed Constants"; } }

		public string Description { get { return "Checks for existing constants removed in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (firstClass.Constants.Any())
			{
				var removed = secondClass.Constants.FindRemovedMembers(firstClass.Constants);

				foreach (var rem in removed)
				{
					changes.Add(new IdentifiedChange
					{
						Priority = 1,// need value... for type taxonomy-like changes,
						Inspector = this.ShortCode,
						Descriptor = new TextDescriptor
						{
							Name = rem.ToString(),
							Message = rem.ToCode()
						}
					});
				}
			}
		}
	}
}

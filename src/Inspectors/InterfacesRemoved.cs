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
	/// <summary>
	/// Looking for changes in the interfaces a type implements.
	/// </summary>
	public class InterfacesRemoved : ITypeInspector
	{
		public bool Enabled { get; set; }

		// need to validate all short codes...
		public string ShortCode { get { return "TI005"; } }

		public string DisplayName { get { return "Removed Interfaces"; } }

		public string Description { get { return "Looks for interfaces removed from a type."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				IReferenceTypeDefinition ref1 = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition ref2 = second as IReferenceTypeDefinition;

				var removed = ref1.Implements.RemovedFrom(ref2.Implements);

				if (removed.Any())
				{
					// or for each - no longer implements...
					foreach (var remove in removed)
					{
						changes.Add(new IdentifiedChange
						{
							Description = "Interface list change",
							Priority = 1,// need value... for type taxonomy-like changes,
							Inspector = this.ShortCode,
							Descriptor = new TextDescriptor { Name = "No longer implements", Message = remove.ToCode() }
						});
					}
				}
			}
		}
	}

}

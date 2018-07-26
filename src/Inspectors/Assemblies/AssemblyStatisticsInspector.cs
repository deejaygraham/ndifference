using NDifference.Analysis;
using NDifference.Inspection;
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
	/// Gathers stats for an assembly
	/// </summary>
	public class AssemblyStatisticsInspector : ITypeCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TCI_ASI"; } }

		public string DisplayName { get { return "Assembly Statistics"; } }

		public string Description { get { return "Gathers Statistics for an assembly"; } }

		public void Inspect(ICombinedTypes types, IdentifiedChangeCollection changes)
		{
			int oldNamespaces = types.Types
				.Where(x => x.First != null)
				.Select(x => x.First.Namespace)
				.Distinct()
				.Count();

			int newNamespaces = types.Types
				.Where(x => x.Second != null)
				.Select(x => x.Second.Namespace)
				.Distinct()
				.Count();
			
			if (oldNamespaces != newNamespaces)
			{
                changes.Add(new IdentifiedChange(this, WellKnownAssemblyCategories.AssemblyInternal, new NamedDeltaDescriptor
				{
					Name = "Namespaces",
					Was = oldNamespaces.ToString(),
					IsNow = newNamespaces.ToString()
				}));
			}

			int oldTypes = types.Types.Count(x => x.First != null);
			int newTypes = types.Types.Count(x => x.Second != null);

			if (oldTypes != newTypes)
			{
                changes.Add(new IdentifiedChange(this, WellKnownAssemblyCategories.AssemblyInternal, new NamedDeltaDescriptor
				{
					Name = "Types",
					Was = oldTypes.ToString(),
					IsNow = newTypes.ToString()
				}));
			}
		}
	}

}

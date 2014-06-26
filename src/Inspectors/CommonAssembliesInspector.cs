using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Looking for changes between common assemblies in two releases
	/// </summary>
	public class CommonAssembliesInspector : IAssemblyCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "ACI002"; } }

		public string DisplayName { get { return "Common Assemblies"; } }

		public string Description { get { return "Checks for common assemblies between two versions"; } }

		public void Inspect(ICombinedAssemblies assemblies, IdentifiedChangeCollection changes)
		{
			Debug.Assert(assemblies != null, "List of assemblies cannot be null");

			changes.Add(WellKnownSummaryCategories.ChangedAssemblies);

			var comparer = new AssemblyNameComparer();

			foreach (var common in assemblies.ChangedInCommon)
			{
				Debug.Assert(common.First != null);
				Debug.Assert(common.Second != null);

				// most common files need to be analysed 
				// further to check for API changes...
				changes.Add(new IdentifiedChange(this, WellKnownSummaryCategories.ChangedAssemblies, common.First.Name, new DocumentLink
				{
					LinkText = common.First.Name,
					LinkUrl = common.First.Name,
					Identifier = common.Second.Identifier
				}));
			}

		}
	}

}

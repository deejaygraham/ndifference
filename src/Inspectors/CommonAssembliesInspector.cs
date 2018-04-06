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

		public void Inspect(ICombinedAssemblies combined, IdentifiedChangeCollection changes)
		{
			Debug.Assert(combined != null, "List of assemblies cannot be null");

            var potentiallyChangedAssemblies = combined.ChangedInCommon;

            if (potentiallyChangedAssemblies.Any())
            {
                changes.Add(WellKnownSummaryCategories.PotentiallyChangedAssemblies);

                foreach (var common in potentiallyChangedAssemblies)
                {
                    Debug.Assert(common.First != null);
                    Debug.Assert(common.Second != null);

                    // TODO - this is a wild guess - number may not reflect the actual number in the report. 
                    changes.Add(new IdentifiedChange(this, WellKnownSummaryCategories.PotentiallyChangedAssemblies, common.First.Name, new DocumentLink
                    {
                        LinkText = common.First.Name,
                        LinkUrl = common.First.Name,
                        Identifier = common.Second.Identifier
                    }));
                }
            }
		}
	}

}

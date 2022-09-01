using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using System.Diagnostics;

namespace NDifference.Inspectors
{
    /// <summary>
    /// Looking for assemblies in first that are not in second. I.e. assemblies that
    /// have been considered unused or obsolete and therefore deleted from a project.
    /// </summary>
    public class RemovedAssembliesInspector : IAssemblyCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "ACI003"; } }

		public string DisplayName { get { return "Removed Assemblies"; } }

		public string Description { get { return "Checks for removed assemblies"; } }

		public void Inspect(ICombinedAssemblies combined, IdentifiedChangeCollection changes)
		{
			Debug.Assert(combined != null, "List of assemblies cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

            var removedAssemblies = combined.InEarlierOnly;

            foreach (var removed in removedAssemblies)
            {
                string assemblyName = removed.First.Name;

				changes.Add(new IdentifiedChange(WellKnownChangePriorities.RemovedAssemblies,
					Severity.BreakingChange,
					new NameDescriptor
					{
						Name = assemblyName,
						Reason = "Removed assembly"
					}));
            }
        }
	}
}

using NDifference.Analysis;
using NDifference.Inspection;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Looking for assemblies that have been added from first to second.
	/// </summary>
	public class AddedAssembliesInspector : IAssemblyCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "ACI_AAI"; } }

		public string DisplayName { get { return "Added Assemblies";  } }

		public string Description { get { return "Checks for added assemblies"; } }

		public void Inspect(ICombinedAssemblies assemblies, IdentifiedChangeCollection changes)
		{
			Debug.Assert(assemblies != null, "List of assemblies cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownSummaryCategories.AddedAssemblies);

			var comparer = new AssemblyNameComparer();

			foreach (var added in assemblies.InLaterOnly)
			{
				changes.Add(new IdentifiedChange(this, WellKnownSummaryCategories.AddedAssemblies, added.Second.Name)); 
			}
		}
	}
}

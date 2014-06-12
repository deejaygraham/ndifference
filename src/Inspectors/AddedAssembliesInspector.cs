using NDifference.Analysis;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Looking for assemblies that have been added from first to second.
	/// </summary>
	public class AddedAssembliesInspector : IAssemblyCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "ACI001"; } }

		public string DisplayName { get { return "Added Assemblies";  } }

		public string Description { get { return "Checks for added assemblies"; } }

		public void Inspect(IEnumerable<IAssemblyDiskInfo> first, IEnumerable<IAssemblyDiskInfo> second, IdentifiedChangeCollection changes)
		{
			Debug.Assert(first != null, "First list of assemblies cannot be null");
			Debug.Assert(second != null, "Second list of assemblies cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownAssemblyCategories.AddedAssemblies);

			var comparer = new AssemblyNameComparer();

			foreach (var added in first.AddedTo(second, comparer))
			{
				changes.Add(new IdentifiedChange 
				{ 
					Description = added.Name, 
					Priority = WellKnownAssemblyCategories.AddedAssemblies.Priority.Value, 
					Inspector = this.ShortCode
				});
			}
		}
	}
}

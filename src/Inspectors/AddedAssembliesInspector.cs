using NDifference.Analysis;
using NDifference;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class AddedAssembliesInspector : IAssemblyCollectionInspector
	{
		public void Inspect(IEnumerable<IAssemblyDiskInfo> first, IEnumerable<IAssemblyDiskInfo> second, IdentifiedChangeCollection changes)
		{
			Debug.Assert(first != null, "First list of assemblies cannot be null");
			Debug.Assert(second != null, "Second list of assemblies cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownAssemblyCategories.AddedAssemblies);

			var comparer = new AssemblyNameComparer();

			// REVIEW - need well known categories for each item... assemblies added, changed, removed, unchanged.
			foreach (var added in first.AddedTo(second, comparer))
			{
				changes.Add(new IdentifiedChange 
				{ 
					Description = added.Name, 
					Priority = WellKnownAssemblyCategories.AddedAssemblies.Priority.Value 
				});
			}
		}
	}
}

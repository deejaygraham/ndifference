using NDifference.Analysis;
using NDifference.Reporting;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	public class CommonAssembliesInspector : IAssemblyCollectionInspector
	{
		public void Inspect(IEnumerable<IAssemblyDiskInfo> first, IEnumerable<IAssemblyDiskInfo> second, IdentifiedChangeCollection changes)
		{
			Debug.Assert(first != null, "First list of assemblies cannot be null");
			Debug.Assert(second != null, "Second list of assemblies cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownAssemblyCategories.ChangedAssemblies);

			var comparer = new AssemblyNameComparer();

			// REVIEW - need well known categories for each item... assemblies added, changed, removed, unchanged.
			//	changes.Add(new IdentifiedChange { Description = added.Name });
			//}

			foreach (var common in first.InCommonWith(second, comparer))
			{
				var oldVersion = first.FindMatchFor(common);
				var newVersion = second.FindMatchFor(common);

				if (oldVersion == null || newVersion == null)
				{
					Debug.Assert(oldVersion != null && newVersion != null, "Mismatch finding common assemblies");
					continue;
				}

				if (oldVersion == newVersion)
				{
					// if there's an exact match in all respects
					// this may be the case if we're using 
					// the same version of a third party library 
					// REVIEW - don't add it...
					changes.Add(new IdentifiedChange { Description = oldVersion.Name + " has NOT changed" } );
				}
				else
				{
					// most common files need to be analysed 
					// further to check for API changes...
					changes.Add(new IdentifiedChange 
					{ 
						Description = oldVersion.Name, 
						Priority = WellKnownAssemblyCategories.ChangedAssemblies.Priority.Value,
						Descriptor = new DocumentLink 
						{ 
							LinkText = oldVersion.Name, 
							LinkUrl = oldVersion.Name, 
							Identifier = newVersion.Identifier 
						}
					});
				}
			}

		}
	}

}

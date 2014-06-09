using NDifference.Analysis;
using NDifference.Reporting;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Looking for changes between common assemblies in two releases
	/// </summary>
	public class CommonAssembliesInspector : IAssemblyCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "ACI001"; } }

		public string DisplayName { get { return "Common Assemblies"; } }

		public string Description { get { return "Checks for common assemblies between two versions"; } }

		public void Inspect(IEnumerable<IAssemblyDiskInfo> first, IEnumerable<IAssemblyDiskInfo> second, IdentifiedChangeCollection changes)
		{
			Debug.Assert(first != null, "First list of assemblies cannot be null");
			Debug.Assert(second != null, "Second list of assemblies cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownAssemblyCategories.ChangedAssemblies);
//			changes.Add(WellKnownAssemblyCategories.UnchangedAssemblies);

			var comparer = new AssemblyNameComparer();

			foreach (var common in first.InCommonWith(second, comparer))
			{
				var oldVersion = first.FindMatchFor(common);
				var newVersion = second.FindMatchFor(common);

				if (oldVersion == null || newVersion == null)
				{
					Debug.Assert(oldVersion != null && newVersion != null, "Mismatch finding common assemblies");
					continue;
				}

				if (oldVersion.Equals(newVersion))
				{
					// if there's an exact match in all respects
					// this may be the case if we're using 
					// the same version of a third party library 
					// REVIEW - don't add it...
					//bool reportUnchanged = false;

					//if (reportUnchanged)
					//{
					//	changes.Add(new IdentifiedChange
					//	{
					//		Description = oldVersion.Name,
					//		Priority = WellKnownAssemblyCategories.UnchangedAssemblies.Priority.Value
					//	});
					//}
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

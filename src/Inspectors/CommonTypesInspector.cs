using NDifference.Analysis;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Checks types in an assembly to find differences used for further analysis.
	/// </summary>
	public class CommonTypesInspector : ITypeCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TCI002"; } }

		public string DisplayName { get { return "Common Types"; } }

		public string Description { get { return "Checks for common types between two versions of the same assembly"; } }

		public void Inspect(IEnumerable<ITypeInfo> first, IEnumerable<ITypeInfo> second, IdentifiedChangeCollection changes)
		{
			Debug.Assert(first != null, "First list of types cannot be null");
			Debug.Assert(second != null, "Second list of types cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownTypeCategories.ChangedTypes);
//			changes.Add(WellKnownTypeCategories.UnchangedTypes);

			var comparer = new TypeNameComparer();

			foreach (var common in first.InCommonWith(second))
			{
				var oldVersion = first.FindMatchFor(common, comparer);
				var newVersion = second.FindMatchFor(common, comparer);

				if (oldVersion == null || newVersion == null)
                {
                    Debug.Assert(oldVersion != null && newVersion != null, "Mismatch finding common objects");
                    continue;
                }

				string oldHash = oldVersion.CalculateHash();
				string newHash = newVersion.CalculateHash();

				if (oldHash.Equals(newHash))
                {
                    // if there's an exact match in all respects -
                    // this may be the case if we're using 
                    // the same version in two separate instances 
					//bool reportUnchanged = false;

					//if (reportUnchanged)
					//{
					//	changes.Add(new IdentifiedChange { Description = newVersion.Name, Priority = WellKnownTypeCategories.UnchangedTypes.Priority.Value });
					//}
                }
                else
                {
					changes.Add(new IdentifiedChange 
					{ 
						Description = newVersion.Name, 
						Priority = WellKnownTypeCategories.ChangedTypes.Priority.Value,
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

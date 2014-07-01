using NDifference.Analysis;
using NDifference.Inspection;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Looking for types removed from an assembly.
	/// </summary>
	public class RemovedTypesInspector : ITypeCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TCI005"; } }

		public string DisplayName { get { return "Removed Types"; } }

		public string Description { get { return "Checks for types removed from an assembly"; } }

		public void Inspect(ICombinedTypes types, IdentifiedChangeCollection changes)
		{
			Debug.Assert(types != null, "List of types cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownAssemblyCategories.RemovedTypes);

			foreach (var removed in types.InEarlierOnly)
			{
				changes.Add(new IdentifiedChange(this, WellKnownAssemblyCategories.RemovedTypes, removed.First.FullName));
			}

		}
	}

}

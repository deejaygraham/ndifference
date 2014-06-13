using NDifference.Analysis;
using NDifference.TypeSystem;
using System.Collections.Generic;
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

		public void Inspect(IEnumerable<ITypeInfo> first, IEnumerable<ITypeInfo> second, IdentifiedChangeCollection changes)
		{
			Debug.Assert(first != null, "First list of types cannot be null");
			Debug.Assert(second != null, "Second list of types cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownTypeCategories.RemovedTypes);

			foreach (var removed in first.RemovedFrom(second))
			{
				changes.Add(new IdentifiedChange
				{
					Description = removed.Name,
					Priority = WellKnownTypeCategories.RemovedTypes.Priority.Value,
					Inspector = this.ShortCode

				});
			}

		}
	}

}

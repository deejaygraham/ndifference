using NDifference.Analysis;
using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Looking for types that have been added between first and second.
	/// </summary>
	public class AddedTypesInspector : ITypeCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TCI001"; } }

		public string DisplayName { get { return "Added Types"; } }

		public string Description { get { return "Checks for added types in an assembly"; } }

		public void Inspect(IEnumerable<ITypeInfo> first, IEnumerable<ITypeInfo> second, IdentifiedChangeCollection changes)
		{
			Debug.Assert(first != null, "First list of types cannot be null");
			Debug.Assert(second != null, "Second list of types cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownTypeCategories.AddedTypes);

			foreach (var added in first.AddedTo(second))
			{
				changes.Add(new IdentifiedChange
				{
					Description = added.Name,
					Priority = WellKnownTypeCategories.AddedTypes.Priority.Value
				});
			}
			
		}
	}
}

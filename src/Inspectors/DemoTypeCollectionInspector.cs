using NDifference.Analysis;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Uncritically takes collections and creates a change for each one.
	/// </summary>
	public class DemoTypeCollectionInspector : ITypeCollectionInspector
	{
		public bool Enabled { get { return false; } set { } }

		public string ShortCode { get { return "TCI003"; } }

		public string DisplayName { get { return "Always creates change for Types"; } }

		public string Description { get { return "Used for debugging on reporting"; } }

		public void Inspect(IEnumerable<ITypeInfo> first, IEnumerable<ITypeInfo> second, IdentifiedChangeCollection changes)
		{
			changes.Add(WellKnownAssemblyCategories.ChangedTypes);

			var comparer = new TypeNameComparer();

			foreach (var common in first.InCommonWith(second))
			{
				var oldVersion = first.FindMatchFor(common, comparer);
				var newVersion = second.FindMatchFor(common, comparer);

				string oldHash = oldVersion.CalculateHash();
				string newHash = newVersion.CalculateHash();

				changes.Add(new IdentifiedChange 
				{ 
					Description = newVersion.FullName,
					Priority = WellKnownAssemblyCategories.ChangedTypes.Priority.Value,
					Descriptor = new DocumentLink
					{
						LinkText = oldVersion.Name,
						LinkUrl = oldVersion.FullName,
						Identifier = newVersion.Identifier
					},
					Inspector = this.ShortCode

				});
			}
		}
	}
}


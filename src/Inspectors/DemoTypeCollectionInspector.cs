using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

		public void Inspect(ICombinedTypes types, IdentifiedChangeCollection changes)
		{
			changes.Add(WellKnownAssemblyCategories.ChangedTypes);

			var comparer = new TypeNameComparer();

			foreach (var common in types.InCommon)
			{
				changes.Add(new IdentifiedChange 
				{
					Description = common.Second.FullName,
					Priority = WellKnownAssemblyCategories.ChangedTypes.Priority.Value,
					Descriptor = new DocumentLink
					{
						LinkText = common.First.Name,
						LinkUrl = common.First.FullName,
						Identifier = common.Second.Identifier
					},
					Inspector = this.ShortCode

				});
			}
		}
	}
}


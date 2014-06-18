using NDifference.Analysis;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Checks for obsolete types in an assembly.
	/// </summary>
	public class ObsoleteTypeInspector : ITypeCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TCI004"; } }

		public string DisplayName { get { return "Obsolete Types"; } }

		public string Description { get { return "Checks for obsolete types in an assembly"; } }
		
		public void Inspect(IEnumerable<ITypeInfo> first, IEnumerable<ITypeInfo> second, IdentifiedChangeCollection changes)
		{
			Debug.Assert(first != null, "First list of types cannot be null");
			Debug.Assert(second != null, "Second list of types cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownAssemblyCategories.ObsoleteTypes);

			foreach (var s in second)
			{
				if (s.ObsoleteMarker != null)
				{
					changes.Add(new IdentifiedChange(this, WellKnownAssemblyCategories.ObsoleteTypes, new TextDescriptor { Name = s.FullName, Message = s.ObsoleteMarker.Message }));
				}
			}

		}
	}
}

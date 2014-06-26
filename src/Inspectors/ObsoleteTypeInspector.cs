using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
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
		
		public void Inspect(ICombinedTypes types, IdentifiedChangeCollection changes)
		{
			Debug.Assert(types != null, "List of types cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownAssemblyCategories.ObsoleteTypes);

			foreach (var s in types.InCommon)
			{
				ITypeInfo t1 = s.First;
				ITypeInfo ti = s.Second;

				if (ti.ObsoleteMarker != null)
				{
					changes.Add(
						new IdentifiedChange(
							this, 
							WellKnownAssemblyCategories.ObsoleteTypes, 
							new TextDescriptor 
							{ 
								Name = ti.FullName, 
								Message = ti.ObsoleteMarker.Message 
							}));
				}
			}

		}
	}
}

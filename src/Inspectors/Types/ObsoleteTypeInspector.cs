﻿using NDifference.Analysis;
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
            
			foreach (var s in types.InCommon)
			{
				ITypeInfo t1 = s.First;
				ITypeInfo ti = s.Second;

				if (t1.ObsoleteMarker == null && ti.ObsoleteMarker != null)
                {
                    var typeMadeObsolete = new IdentifiedChange(WellKnownChangePriorities.ObsoleteTypes,
						Severity.BreakingChange,
						new NameValueDescriptor 
                        { 
                            Name = ti.FullName, 
                            Value = ti.ObsoleteMarker.Message 
                        });

                    typeMadeObsolete.ForType(t1);

                    changes.Add(typeMadeObsolete);
                }
			}
		}
	}
}

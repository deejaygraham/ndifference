using System;
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
            
			foreach (var s in types.InCommon)
			{
				ITypeInfo t1 = s.First;
				ITypeInfo ti = s.Second;

				// don't care about previously obsolete...
				if (ti.ObsoleteMarker != null)
                {
                    string reason = ti.ObsoleteMarker.Message;

                    if (String.IsNullOrEmpty(reason))
                        reason = "No specific message given";

                    var typeMadeObsolete = new IdentifiedChange(WellKnownChangePriorities.ObsoleteTypes,
						Severity.BreakingChange,
						new NameValueDescriptor 
                        { 
                            Name = ti.FullName, 
                            Reason = reason 
                        });

                    typeMadeObsolete.ForType(t1);

                    changes.Add(typeMadeObsolete);
                }
			}
		}
	}
}

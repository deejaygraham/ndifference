using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Looking for assemblies that have been added from first to second. I.e. New assemblies added to the
	/// project since the last version.
	/// </summary>
	public class AddedAssembliesInspector : IAssemblyCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "ACI_AAI"; } }

		public string DisplayName { get { return "New Assemblies";  } }

		public string Description { get { return "Checks for new assemblies"; } }

		public void Inspect(ICombinedAssemblies combined, IdentifiedChangeCollection changes)
		{
			Debug.Assert(combined != null, "List of assemblies cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");
            
            var addedAssemblies = combined.InLaterOnly;

            foreach (var added in addedAssemblies)
            {
                string assemblyName = added.Second.Name;

				// descriptor ?
				changes.Add(new IdentifiedChange(WellKnownChangePriorities.AddedAssemblies, 
					new NameDescriptor 
					{ 
						Name = assemblyName,
						Reason = "Assembly added"
					}));
            }
        }
	}
}

using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using System.Collections.Generic;

namespace NDifference.Inspectors
{
    /// <summary>
    /// Checks for changes in the assemblies referenced in each version
    /// and reports additions or removals.
    /// </summary>
    public class AssemblyReferenceInspector : IAssemblyInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "AI_ACRI"; } }

		public string DisplayName { get { return "Assembly Reference Changes"; } }

		public string Description { get { return "Checks for changes to an assembly's reference list"; } }

		public void Inspect(IAssemblyInfo first, IAssemblyInfo second, IdentifiedChangeCollection changes)
		{
			var comparer = new AssemblyReferenceComparer();

			// added
			var refsAdded = first.References.AddedTo(second.References, comparer);

			foreach (var difference in refsAdded)
			{
				changes.Add(new IdentifiedChange(WellKnownChangePriorities.AddedReferences, 
					new NameDescriptor
					{
						Name = difference.Name,
						Reason = "Reference added"
					}));
			}

			var refsRemoved = first.References.RemovedFrom(second.References, comparer);

			foreach (var difference in refsRemoved)
			{
				changes.Add(new IdentifiedChange(WellKnownChangePriorities.RemovedReferences, new NameDescriptor
				{
					Name = difference.Name,
					Reason = "Reference removed"
				}));
			}
		}
	}


	internal class AssemblyReferenceComparer : IEqualityComparer<AssemblyReference>
	{
		public bool Equals(AssemblyReference x, AssemblyReference y)
		{
			return x.Name.Equals(y.Name);
		}

		public int GetHashCode(AssemblyReference obj)
		{
			return obj.Name.GetHashCode();
		}
	}
}

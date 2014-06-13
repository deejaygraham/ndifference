using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using System;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Checks for changes in target architecture - x86 -> x64 | Any CPU
	/// </summary>
	public class ArchitectureChangeInspector : IAssemblyInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "AI001"; } }

		public string DisplayName { get { return "Assembly Architecture Changes"; } }

		public string Description { get { return "Checks for changes to an assembly's architecture"; } }

		public void Inspect(IAssemblyInfo first, IAssemblyInfo second, IdentifiedChangeCollection changes)
		{
			changes.Add(WellKnownTypeCategories.AssemblyInternal);

			if (first.Architecture != second.Architecture)
			{
				// need to report this change...
				changes.Add(new IdentifiedChange
				{
					Description = String.Format("Architecture has changed from {0} to {1}",
						first.Architecture,
						second.Architecture),
					Priority = WellKnownChangePriorities.AssemblyInternal,
					Inspector = this.ShortCode,
					Descriptor = new DeltaDescriptor
					{
						Name = "Architecture has changed",
						Was = first.Architecture,
						IsNow = second.Architecture
					}
				});
			}
		}
	}
}

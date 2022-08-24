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

		public string ShortCode { get { return "AI_ACI"; } }

		public string DisplayName { get { return "Assembly Architecture Changes"; } }

		public string Description { get { return "Checks for changes to an assembly's architecture"; } }

		public void Inspect(IAssemblyInfo first, IAssemblyInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Architecture != second.Architecture)
			{
                // need to report this change...
                changes.Add(new IdentifiedChange(WellKnownChangePriorities.AssemblyInternal, 
					new DeltaDescriptor
					{
						Reason = "Architecture has changed",
						Was = first.Architecture,
						IsNow = second.Architecture
					}));
			}
		}
	}
}

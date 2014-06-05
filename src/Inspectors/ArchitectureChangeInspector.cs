using NDifference.Analysis;
using System;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Checks for changes in target architecture - x86 -> x64 | Any CPU
	/// </summary>
	public class ArchitectureChangeInspector : IAssemblyInspector
	{
		public void Inspect(IAssemblyInfo first, IAssemblyInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Architecture != second.Architecture)
			{
				// need to report this change...
				changes.Add(new IdentifiedChange 
				{
					Description = String.Format("Architecture has changed from {0} to {1}", 
						first.Architecture, 
						second.Architecture)
				});
			}
		}
	}
}

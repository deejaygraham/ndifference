
namespace NDifference.Inspectors
{
	/// <summary>
	/// Checks for changes in target architecture - x86 -> x64 | Any CPU
	/// </summary>
	public class ArchitectureChangeInspector : IAssemblyInspector
	{
		public void Inspect(IAssemblyInfo first, IAssemblyInfo second)
		{
			if (first.Architecture != second.Architecture)
			{
				// need to report this change...
			}
		}
	}
}

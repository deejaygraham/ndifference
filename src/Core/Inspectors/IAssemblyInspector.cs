using NDifference.Analysis;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Inspects assemblies and reports differences as an IdentifiedChangeCollection.
	/// </summary>
	public interface IAssemblyInspector
	{
		void Inspect(IAssemblyInfo first, IAssemblyInfo second, IdentifiedChangeCollection changes);
	}
}

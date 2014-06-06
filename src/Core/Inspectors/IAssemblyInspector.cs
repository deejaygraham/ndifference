using NDifference.Analysis;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Inspects assemblies and reports differences as an IdentifiedChangeCollection.
	/// </summary>
	public interface IAssemblyInspector : IInspector
	{
		// REVIEW - do we need a unique id for each one /short code and a on/off switch controlled by project

		void Inspect(IAssemblyInfo first, IAssemblyInfo second, IdentifiedChangeCollection changes);
	}
}

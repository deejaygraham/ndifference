using NDifference.Analysis;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Inspects individual assemblies one by one and reports differences into an IdentifiedChangeCollection.
	/// </summary>
	public interface IAssemblyInspector : IInspector
	{
		// REVIEW - do we need a unique id for each one /short code and a on/off switch controlled by project

        /// <summary>
		/// Compare the first and second versions of the assembly (both must exist) and report differences
		/// into the changes collection.
		/// </summary>
		/// <param name="first">The previous version of the assembly</param>
		/// <param name="second">The new version of the assembly</param>
		/// <param name="changes">The current change collection</param>
		void Inspect(IAssemblyInfo first, IAssemblyInfo second, IdentifiedChangeCollection changes);
	}
}

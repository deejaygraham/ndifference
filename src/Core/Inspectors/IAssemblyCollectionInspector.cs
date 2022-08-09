using NDifference.Analysis;
using NDifference.Inspection;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Inspects a collection of assemblies and looks for differences between
	/// them. E.g. New assemblies that have been added, old or obsolete assemblies
	/// that have been removed. 
	/// </summary>
	public interface IAssemblyCollectionInspector : IInspector
	{
		/// <summary>
		/// Inspect the collection and report changes identified into the changes collection.
		/// </summary>
		/// <param name="combined">The assemblies</param>
		/// <param name="changes">The list of current changes</param>
		void Inspect(ICombinedAssemblies combined, IdentifiedChangeCollection changes);
	}
}

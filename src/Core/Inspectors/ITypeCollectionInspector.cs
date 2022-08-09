using NDifference.Analysis;
using NDifference.Inspection;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Inspect a collection of types found in a single assembly.
	/// </summary>
	public interface ITypeCollectionInspector : IInspector
	{
		/// <summary>
		/// Inspect collections of types from a single assembly to e.g.
		/// document the type which have been removed, added or marked as obsolete.
		/// Changes added to collection.
		/// </summary>
		/// <param name="types"></param>
		/// <param name="changes"></param>
		void Inspect(ICombinedTypes types, IdentifiedChangeCollection changes);
	}
}

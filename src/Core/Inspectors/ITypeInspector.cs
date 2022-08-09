using NDifference.Analysis;
using NDifference.TypeSystem;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Inspects two versions of a specific type in an assembly.
	/// </summary>
	public interface ITypeInspector : IInspector
	{
		/// <summary>
		/// Inspect differences between two version of a type and add changes to the collection.
		/// </summary>
		/// <param name="first">The previous version of the type</param>
		/// <param name="second">The new version of the type</param>
		/// <param name="changes">Change collection</param>
		void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes);
	}
}

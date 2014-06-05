using NDifference.Analysis;
using NDifference.TypeSystem;

namespace NDifference.Inspectors
{
	public interface ITypeInspector
	{
		void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes);
	}
}

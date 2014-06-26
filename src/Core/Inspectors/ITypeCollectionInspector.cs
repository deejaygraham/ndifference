using NDifference.Analysis;
using NDifference.Inspection;

namespace NDifference.Inspectors
{
	public interface ITypeCollectionInspector : IInspector
	{
		void Inspect(ICombinedTypes types, IdentifiedChangeCollection changes);
	}
}

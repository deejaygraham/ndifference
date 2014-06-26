using NDifference.Analysis;
using NDifference.Inspection;

namespace NDifference.Inspectors
{
	public interface IAssemblyCollectionInspector : IInspector
	{
		void Inspect(ICombinedAssemblies combined, IdentifiedChangeCollection changes);
	}
}

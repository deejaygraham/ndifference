using NDifference.Analysis;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public interface ITypeCollectionInspector : IInspector
	{
		void Inspect(IEnumerable<ITypeInfo> first, IEnumerable<ITypeInfo> second, IdentifiedChangeCollection changes);
	}
}

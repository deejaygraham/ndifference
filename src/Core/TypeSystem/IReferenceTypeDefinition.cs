using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	public interface IReferenceTypeDefinition
	{
		List<FullyQualifiedName> Implements { get; set; }

		//List<IMethod> Methods { get; set; }

		//List<Property> Properties { get; set; }

		List<MemberEvent> Events { get; set; }

		//List<Indexer> Indexers { get; set; }
	}
}

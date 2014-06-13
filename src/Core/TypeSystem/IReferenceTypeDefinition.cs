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

		List<IMemberMethod> Methods { get; set; }

		List<MemberProperty> Properties { get; set; }

		List<MemberEvent> Events { get; set; }

		List<Indexer> Indexers { get; set; }
	}
}

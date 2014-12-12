using NDifference.Inspection;
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

		List<IMemberMethod> AllMethods { get; set; }

		List<IMemberMethod> Methods(MemberVisibilityOption accessibility);

		List<MemberProperty> AllProperties { get; set; }

		List<MemberProperty> Properties(MemberVisibilityOption accessibility);

		List<MemberEvent> Events { get; set; }

		List<Indexer> Indexers { get; set; }
	}
}

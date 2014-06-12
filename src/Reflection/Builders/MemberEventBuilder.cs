using Mono.Cecil;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reflection.Builders
{
	public class MemberEventBuilder
	{
		public void BuildFrom(TypeDefinition discovered, IReferenceTypeDefinition building)
		{
			Debug.Assert(discovered != null, "Type definition is null");
			Debug.Assert(building != null, "Class definition is null");

			if (discovered.HasEvents)
			{
				foreach (var e in discovered.Events)
				{
					building.Events.Add(BuildFrom(e));
				}
			}
		}

		public MemberEvent BuildFrom(EventDefinition ed)
		{
			Debug.Assert(ed != null, "Event definition is null");

			var builtEvent = new MemberEvent();

			builtEvent.Name = ed.Name;
			builtEvent.EventType = new FullyQualifiedName(ed.EventType.FriendlyName());
			builtEvent.Accessibility = MemberAccessibility.Public;

			var obsBuilder = new ObsoleteBuilder();
			obsBuilder.BuildFrom(ed, builtEvent);

			return builtEvent;
		}
	}

}

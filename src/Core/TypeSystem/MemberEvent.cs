using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	[Serializable]
	public class MemberEvent : IMemberInfo
	{
		public string Name { get; set; }

		public FullyQualifiedName EventType { get; set; }

		public MemberAccessibility Accessibility { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public SourceCode ToCode()
		{
			return null;
		}
	}
}

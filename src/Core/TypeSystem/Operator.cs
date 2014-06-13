using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	[Serializable]
	public class Operator : IMemberInfo
	{
		public MemberAccessibility Accessibility { get; set; }

		public ICoded ToCode()
		{
			return new SourceCode();
		}

		public Obsolete ObsoleteMarker { get; set; }
	}
}

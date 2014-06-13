using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// Represents an implementation of processing performed by an object or class.
	/// </summary>
	[Serializable]
	public class MemberMethod : IMemberMethod
	{
		public MemberAccessibility Accessibility { get; set; }

		public ICoded ToCode()
		{
			return null;
		}

		public Signature Signature { get; set; }

		public FullyQualifiedName ReturnType { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public bool IsAbstract { get; set; }

		public bool IsStatic { get; set; }
	}

}

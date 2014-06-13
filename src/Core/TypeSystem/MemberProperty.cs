using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// Represents a property on a class
	/// </summary>
	[DebuggerDisplay("property {Name}")]
	[Serializable]
	public class MemberProperty : IMemberInfo
	{
		public string Name { get; set; }

		public FullyQualifiedName PropertyType { get; set; }

		public MemberAccessibility GetterAccessibility { get; set; }

		public MemberAccessibility SetterAccessibility { get; set; }

		public MemberAccessibility Accessibility { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public ICoded ToCode()
		{
			return null;
		}
	}
}

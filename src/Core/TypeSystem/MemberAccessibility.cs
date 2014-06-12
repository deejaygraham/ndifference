using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// Accessibility of type member.
	/// </summary>
	public enum MemberAccessibility
	{
		/// <summary>
		/// Limited to containing type.
		/// </summary>
		Private,
		/// <summary>
		/// Not limited
		/// </summary>
		Public,
		/// <summary>
		/// Limited to containing type or types derived from containing type
		/// </summary>
		Protected,
		/// <summary>
		/// Limited to assembly
		/// </summary>
		Internal,
		/// <summary>
		/// Limited to assembly or types derived from containing type
		/// </summary>
		ProtectedInternal
	}
}

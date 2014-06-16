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
	/// Finalize instance of a class. 
	/// </summary>
	[DebuggerDisplay("~{Name.Type.Name}")]
	[Serializable]
	public class Finalizer : IMemberInfo
	{
		public Finalizer(string name)
		{
			this.Name = new FullyQualifiedName(name);
		}

		public FullyQualifiedName Name { get; set; }

		/// <summary>
		/// Should not have accessibility but treated for our purposes as if public.
		/// </summary>
		public MemberAccessibility Accessibility { get { return MemberAccessibility.Public; } }

		public Obsolete ObsoleteMarker { get; set; }

		public override string ToString()
		{
			return string.Format(
				 "~{0}()",
				 this.Name.Type.Value);
		}

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(new PunctuationTag("~"));
			code.Add(this.Name.ToCode());
			code.Add(new PunctuationTag("("));
			code.Add(new PunctuationTag(")"));

			return code;
		}
	}
}

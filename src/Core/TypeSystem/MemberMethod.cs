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

		public Signature Signature { get; set; }

		public FullyQualifiedName ReturnType { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public bool IsAbstract { get; set; }

		public bool IsStatic { get; set; }

		public override string ToString()
		{
			return string.Format(
							"{0}{1} {2} {3}",
							this.IsStatic ? "static " : string.Empty,
							this.IsAbstract ? "abstract " : string.Empty,
							this.ReturnType,
							this.Signature);
		}

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			if (this.IsStatic)
			{
				code.Add(new KeywordTag("static"));
			}

			if (this.IsAbstract)
			{
				code.Add(new KeywordTag("abstract"));
			}

			code.Add(this.ReturnType.ToCode());
			code.Add(this.Signature.ToCode());

			return code;
		}

		public bool ExactlyMatches(IMemberMethod other)
		{
			return string.Compare(
				this.ToString(),
				other.ToString(),
				StringComparison.Ordinal) == 0;
		}

		public bool FuzzyMatches(IMemberMethod other)
		{
			return this.Signature.FuzzyMatches(other.Signature);
		}
	}
}

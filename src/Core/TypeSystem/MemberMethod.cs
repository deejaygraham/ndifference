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

		public bool IsVirtual { get; set; }

		public override string ToString()
		{
			return string.Format(
							"{0}{1} {2} {3} {4}",
							this.IsStatic ? "static " : string.Empty,
							this.IsAbstract ? "abstract " : string.Empty,
							this.IsVirtual ? "virtual" : string.Empty,
							this.ReturnType,
							this.Signature);
		}

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			if (this.IsStatic)
			{
				code.Add(new KeywordTag("static"));
                code.Add(new WhitespaceTag());
			}

			if (this.IsAbstract)
			{
				code.Add(new KeywordTag("abstract"));
                code.Add(new WhitespaceTag());
			}

			if (this.IsVirtual)
			{
				code.Add(new KeywordTag("virtual"));
                code.Add(new WhitespaceTag());
			}

			code.Add(this.ReturnType.ToCode());
            code.Add(new WhitespaceTag());

			code.Add(this.Signature.ToCode());

			return code;
		}

		public bool ExactlyMatches(IMemberMethod other)
		{
			return this.Signature.ExactlyMatches(other.Signature);
		}

		public bool FuzzyMatches(IMemberMethod other)
		{
			return this.Signature.FuzzyMatches(other.Signature);
		}
	}
}

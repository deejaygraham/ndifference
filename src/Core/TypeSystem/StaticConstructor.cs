using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// Initializes a class.
	/// </summary>
	[Serializable]
	public class StaticConstructor : IMemberInfo
	{
		public StaticConstructor(string name)
		{
			this.Name = new FullyQualifiedName(name);
		}

		public FullyQualifiedName Name { get; set; }

		/// <summary>
		/// For our purposes static constructors behave as if they were public.
		/// </summary>
		public MemberAccessibility Accessibility
		{
			get
			{
				return MemberAccessibility.Public;
			}
		}

		public override string ToString()
		{
			return string.Format(
				"static {0}()",
				this.Name.Type.Value);
		}

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(new KeywordTag("static"));
			code.Add(this.Name.ToCode());
			code.Add(new PunctuationTag("("));
			code.Add(new PunctuationTag(")"));

			return code;
		}

		public Obsolete ObsoleteMarker { get; set; }
	}
}

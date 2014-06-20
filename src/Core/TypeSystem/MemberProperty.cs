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
	public class MemberProperty : IMemberInfo, IMatchExactly<MemberProperty>, IMatchFuzzily<MemberProperty>
	{
		public string Name { get; set; }

		public FullyQualifiedName PropertyType { get; set; }

		public MemberAccessibility GetterAccessibility { get; set; }

		public MemberAccessibility SetterAccessibility { get; set; }

		public MemberAccessibility Accessibility { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(this.PropertyType.ToCode());
			code.Add(new IdentifierTag(this.Name));

			if (this.GetterAccessibility == MemberAccessibility.Public 
				|| this.SetterAccessibility == MemberAccessibility.Public)
			{
				code.Add(new PunctuationTag("{"));

				if (this.GetterAccessibility == MemberAccessibility.Public)
				{
					code.Add(new KeywordTag("get;"));
				}
				else if (this.GetterAccessibility == MemberAccessibility.Private)
				{
					code.Add(new KeywordTag("private set;"));
				}

				if (this.SetterAccessibility == MemberAccessibility.Public)
				{
					code.Add(new KeywordTag("set;"));
				}
				else if (this.SetterAccessibility == MemberAccessibility.Private)
				{
					code.Add(new KeywordTag("private set;"));
				}

				code.Add(new PunctuationTag("}"));
			}

			return code;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.AppendFormat("{0} {1} ", this.PropertyType.Type, this.Name);

			if (this.GetterAccessibility == MemberAccessibility.Public
				|| this.SetterAccessibility == MemberAccessibility.Public)
			{
				builder.Append("{ ");

				if (this.GetterAccessibility == MemberAccessibility.Public)
				{
					builder.Append("get; ");
				}
				else if (this.GetterAccessibility == MemberAccessibility.Private)
				{
					builder.Append("private get; ");
				}

				if (this.SetterAccessibility == MemberAccessibility.Public)
				{
					builder.Append("set; ");
				}
				else if (this.SetterAccessibility == MemberAccessibility.Private)
				{
					builder.Append("private set; ");
				}

				builder.Append("} ");
			}

			return builder.ToString();
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public bool ExactlyMatches(MemberProperty other)
		{
			return string.Compare(
				this.ToString(),
				other.ToString(),
				StringComparison.Ordinal) == 0;
		}

		public bool FuzzyMatches(MemberProperty other)
		{
			// matches if the names are the same
			return string.Compare(
				this.Name,
				other.Name,
				StringComparison.Ordinal) == 0;
		}

	}
}

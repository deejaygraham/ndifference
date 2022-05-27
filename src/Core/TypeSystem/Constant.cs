﻿using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// Represents a constant value
	/// </summary>
	[Serializable]
	public class Constant : IMemberInfo, IMatchExactly<Constant>, IMatchFuzzily<Constant>
	{
		public string Name { get; set; }

		public FullyQualifiedName ConstantType { get; set; }

		public MemberAccessibility Accessibility { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(new KeywordTag("const"));
			code.Add(new WhitespaceTag());
			code.Add(this.ConstantType.ToCode());
            code.Add(new WhitespaceTag());
			code.Add(new IdentifierTag(this.Name));

			return code;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.AppendFormat(
				"{0} {1}",
				this.ConstantType.Type,
				this.Name);

			return builder.ToString();
		}

		public bool ExactlyMatches(Constant other)
		{
			return string.Compare(
				this.ToString(),
				other.ToString(),
				StringComparison.Ordinal) == 0;
		}

		public bool FuzzyMatches(Constant other)
		{
			// matches if the names are the same
			return string.Compare(
				this.Name,
				other.Name,
				StringComparison.Ordinal) == 0;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public enum SyntaxKind
	{
		// primitives
		Unknown,
		Identifier,
		Keyword,
		Punctuation, // comma, bracket ?
		MemberName,
		TypeName,
		// higher level items
		EventDeclaration
	}
}

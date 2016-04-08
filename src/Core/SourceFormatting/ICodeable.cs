using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public interface ICodeable
	{
		SyntaxNode ToCode();
	}
}

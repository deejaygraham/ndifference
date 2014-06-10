using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public class KeywordTag : SourceCodeTag
	{
		public KeywordTag()
		{
		}

		public KeywordTag(string name)
			: base("kw", name)
		{
		}
	}
}

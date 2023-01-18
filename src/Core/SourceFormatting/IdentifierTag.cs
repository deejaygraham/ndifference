using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public class IdentifierTag : SourceCodeTag
	{
		public IdentifierTag()
		{
		}

		public IdentifierTag(string name)
			: base("ident", Boldify(name))
		{
		}

        private static string Boldify(string text)
        {
            return "**" + text + "**";
        }
	}
}

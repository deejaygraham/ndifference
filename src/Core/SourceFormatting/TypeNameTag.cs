using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public class TypeNameTag : SourceCodeTag
	{
		public TypeNameTag()
		{
		}

		public TypeNameTag(string name)
			: base("tn", name.Replace("<", "&lt;").Replace(">", "&gt;"))
		{
		}
	}
}

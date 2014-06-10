﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public class PunctuationTag : SourceCodeTag
	{
		public PunctuationTag()
		{
		}

		public PunctuationTag(string name)
			: base("punc", name)
		{
		}
	}

}

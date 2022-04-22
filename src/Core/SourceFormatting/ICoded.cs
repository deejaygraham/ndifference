﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public interface ICoded
	{
		// syntax tree of objects
		string ToXml();

        string ToPlainText();
    }
}

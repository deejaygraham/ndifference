using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class CodeDescriptor : ICodeDescriptor
	{
		public ICoded Code { get; set; }
	}
}

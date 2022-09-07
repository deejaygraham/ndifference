using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public interface ICodeDescriptor : IDescriptor
	{
		ICoded Code { get; }

		//string TypeName { get; }

		//string AssemblyName { get; }
	}
}

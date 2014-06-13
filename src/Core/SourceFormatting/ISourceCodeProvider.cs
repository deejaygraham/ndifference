using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	/// <summary>
	/// An object can provide a source code representation of itself.
	/// </summary>
	public interface ISourceCodeProvider
	{
		ICoded ToCode();
	}
}

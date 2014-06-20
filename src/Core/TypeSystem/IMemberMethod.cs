using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	public interface IMemberMethod : IMemberInfo, ISourceCodeProvider, IMatchExactly<IMemberMethod>, IMatchFuzzily<IMemberMethod>
	{
		Signature Signature { get; }

		bool IsAbstract { get; }

		bool IsStatic { get; }
	}
}

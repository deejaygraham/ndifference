using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	public interface IMemberInfo : ISourceCodeProvider, IMaybeObsolete
	{
		MemberAccessibility Accessibility { get; }
	}
}

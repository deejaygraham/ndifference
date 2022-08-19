using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
    /// <summary>
    /// Describes a change that includes previous value and current value.
    /// </summary>
	public interface IDeltaDescriptor : IDescriptor
	{
		object Was { get; }

		object IsNow { get; }
	}
}

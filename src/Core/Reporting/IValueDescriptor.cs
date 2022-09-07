using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	[Obsolete("IValueDescriptor not used")]
	public interface IValueDescriptor : IDescriptor
	{
		object Value { get; set; }
	}
}

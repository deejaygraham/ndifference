using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public interface IDeltaDescriptor
	{
		string Name { get; }

		object Was { get; }

		object IsNow { get; }

	}
}

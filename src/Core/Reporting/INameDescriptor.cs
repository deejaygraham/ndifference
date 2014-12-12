using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public interface INameDescriptor : IDescriptor
	{
		string Name { get; set; }
	}
}

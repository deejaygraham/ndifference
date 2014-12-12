using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class NameValueDescriptor : INameValueDescriptor
	{
		public string Name { get; set; }

		public object Value { get; set; }

		public int Columns { get { return 2; } }
	}
}

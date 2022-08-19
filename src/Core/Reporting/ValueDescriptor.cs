using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class ValueDescriptor : IValueDescriptor
	{
		public object Value { get; set; }

		public int Columns { get { return 1; } }

		public IEnumerable<string> ColumnNames
		{
			get
			{
				return new string[] { "Signature" };
			}
		}
	}
}

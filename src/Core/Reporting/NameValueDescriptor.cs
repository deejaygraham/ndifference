using System.Collections.Generic;

namespace NDifference.Reporting
{
	public class NameValueDescriptor : INameValueDescriptor
	{
		public string Name { get; set; }

		public object Value { get; set; }

		public int Columns { get { return 2; } }

		public IEnumerable<string> ColumnNames
		{
			get
			{
				return new List<string>();
			}
		}
	}
}

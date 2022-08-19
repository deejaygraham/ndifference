using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class DeltaDescriptor : IDeltaDescriptor
	{
		// public string Name { get; set; }

		public object Was { get; set; }

		public object IsNow { get; set; }

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

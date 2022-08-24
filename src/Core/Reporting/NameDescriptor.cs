using System.Collections.Generic;

namespace NDifference.Reporting
{
    public class NameDescriptor : INameDescriptor
    {
        public string Name { get; set; }

        public string Reason { get; set; }

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

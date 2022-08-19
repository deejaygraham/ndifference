
using System.Collections.Generic;

namespace NDifference.Reporting
{
	public class DocumentLink : IDocumentLink
	{
		public string Identifier { get; set; }

		public string LinkText { get; set; }

		public string LinkUrl { get; set; }

		public int Columns { get { return 1; } }

		public IEnumerable<string> ColumnNames
		{
			get
			{
				return new List<string>();
			}
		}
	}
}

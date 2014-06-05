using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class DocumentLink : IDocumentLink
	{
		public string Identifier { get; set; }

		public string LinkText { get; set; }

		public string LinkUrl { get; set; }
	}
}

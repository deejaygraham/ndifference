using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.UnitTests
{
	public class InMemoryReportOutput : IReportOutput
	{
		public string Folder { get; set; }

		public string Path { get; set; }

		public string Content { get; private set; }

		public void Execute(string reportContent)
		{
			this.Content = reportContent;
		}
	}
}

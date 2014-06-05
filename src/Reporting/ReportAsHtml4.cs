using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class ReportAsHtml4 : IReportFormat
	{
		public ReportAsHtml4()
		{
			this.Identifier = new Identifier().ToString();
		}

		public string Identifier { get; private set; }

		public string FriendlyName
		{
			get
			{
				return "Html";
			}
		}

		public string Extension { get { return ".html"; } }

		public string FormatLink(string link, string text)
		{
			return string.Format(CultureInfo.CurrentUICulture, "<a href=\"{0}\">{1}</a>", link, text);
		}

		public bool Supports(IReportFormat other)
		{
			return String.Compare(other.FriendlyName, this.FriendlyName, ignoreCase: true) == 0;
		}

		public bool Supports(string other)
		{
			return String.Compare(other, this.FriendlyName, ignoreCase: true) == 0;
		}

		public override string ToString()
		{
			return String.Format("{0} ({1})", this.FriendlyName, this.Extension);
		}
	}
}

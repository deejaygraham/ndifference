using NDifference.SourceFormatting;
using System;
using System.Globalization;
using System.Text;

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

		public string Format(IDocumentLink link)
		{
			return string.Format(CultureInfo.CurrentUICulture, "<a id=\"{0}\" href=\"{1}\">{2}</a>", link.Identifier, link.LinkUrl, link.LinkText);
		}

		public string Format(ICoded source)
		{
			var builder = new StringBuilder(source.ToXml());

			builder.Replace("<SourceCode>", "<code>");
			builder.Replace("</SourceCode>", "</code>");

			builder.Replace("<punc>", "<span class=\"punc\">");
			builder.Replace("</punc>", "</span>");

			builder.Replace("<ident>", "<span class=\"ident\">");
			builder.Replace("</ident>", "</span>");

			builder.Replace("<kw>", "<span class=\"kw\">");
			builder.Replace("</kw>", "</span>");

			builder.Replace("<tn>", "<span class=\"tn\">");
			builder.Replace("</tn>", "</span>");

			return builder.ToString();
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

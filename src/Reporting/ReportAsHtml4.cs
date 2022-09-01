using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
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

        public string FormatComment(string text)
        {
			return "<!-- " + text + " -->";
		}

        public string FormatLink(string url, string name)
        {
			var builder = new StringBuilder();

			builder.Append("<a ");
			builder.AppendFormat("href=\"{0}\" ", url.Replace('\\', '/'));
			builder.AppendFormat("title=\"{0}\" ", name);
			builder.Append(">");
			builder.Append(name);
			builder.Append("</a>");

			return builder.ToString();
		}

        public string FormatTableHeader(IEnumerable<string> headings)
        {
			var builder = new StringBuilder();

			builder.AppendLine("<tr>");

			foreach (var heading in headings)
			{
				builder.AppendFormat("<th>{0}</th>", heading);
				builder.AppendLine();
			}

			builder.AppendLine("</tr>");

			return builder.ToString();
		}

        public string FormatTableRow(IEnumerable<string> cells)
        {
			var builder = new StringBuilder();

			builder.AppendLine("<tr>");

			foreach(var cell in cells)
            {
				builder.AppendFormat("<td>{0}</td>", cell);
				builder.AppendLine();
			}

			builder.AppendLine("</tr>");

			return builder.ToString();
		}

        public string FormatTitle(int size, string title, string id)
        {
			var builder = new StringBuilder();

			builder.AppendFormat("<h{0} ");

			if (!String.IsNullOrEmpty(id))
				builder.AppendFormat("id=\"{0}\" ", id);

			builder.AppendFormat(">{0}</h{1}>", title, size);

			return builder.ToString();
		}

		public bool Supports(IReportFormat other)
		{
			return String.Compare(other.FriendlyName, this.FriendlyName, ignoreCase: true) == 0
				|| String.Compare(other.Extension, this.Extension, ignoreCase: true) == 0;
		}

		/// <summary>
		/// Check friendly name or file extension
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Supports(string other)
		{
			return String.Compare(other, this.FriendlyName, ignoreCase: true) == 0
				|| String.Compare(other, this.Extension, ignoreCase: true) == 0;
		}

		public override string ToString()
		{
			return String.Format("{0} ({1})", this.FriendlyName, this.Extension);
		}
	}
}

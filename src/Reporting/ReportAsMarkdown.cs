using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NDifference.Reporting
{
    public class ReportAsMarkdown : IReportFormat
    {
        public ReportAsMarkdown()
        {
            this.Identifier = new Identifier().ToString();
        }

        public string Identifier { get; private set; }

        public string FriendlyName
        {
            get
            {
                return "Markdown";
            }
        }

        public string Extension { get { return ".md"; } }

        /// <summary>
        /// Format a link with an id
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public string Format(IDocumentLink link)
        {
            return string.Format(CultureInfo.CurrentUICulture, "[{2}]({1}) {{ #{0} }}", link.Identifier, link.LinkUrl, link.LinkText);
        }

        public string Format(ICoded source)
        {
            return "`"  + source.ToPlainText() + "`";
        }

        public string FormatLink(string url, string name)
        {
            return string.Format("[{0}]({1})", name, url);
        }

        public string FormatTitle(int size, string title, string id)
        {
            if (size < 1 || size > 6) throw new ArgumentOutOfRangeException();

            var builder = new StringBuilder();

            //builder.AppendLine();
            //builder.AppendLine();

            builder.AppendFormat("{0} {1} ", new string('#', size), title);

            if (!String.IsNullOrEmpty(id))
                builder.AppendFormat("{{ #{0} }}", id);

            //builder.AppendLine();
            //builder.AppendLine();

            return builder.ToString();
        }

        public string FormatComment(string text)
        {
            return "<!-- " + text + " -->";
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

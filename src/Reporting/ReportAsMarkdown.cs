using NDifference.SourceFormatting;
using System;
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

        public string Format(IDocumentLink link)
        {
            return string.Format(CultureInfo.CurrentUICulture, "[{2}]({1}) {{ #{0} }}", link.Identifier, link.LinkUrl, link.LinkText);
        }

        public string Format(ICoded source)
        {
            return "``` "  + source.ToPlainText() + " ```";
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

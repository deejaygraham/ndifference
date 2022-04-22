﻿using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
    public class MarkdownWriter : IDisposable
    {
        private TextWriter writer;

        private MarkdownWriter()
        {
        }

        public void Dispose()
        {
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
        }

        public void Close()
        {
            if (writer != null)
            {
                writer.Close();
                writer = null;
            }
        }

        public static MarkdownWriter Create(TextWriter w)
        {
            return new MarkdownWriter
            {
                writer = w
            };
        }

        public void WriteFrontMatter(IDictionary<string, string> dictionary)
        {
            writer.WriteLine("---");

            foreach (var key in dictionary.Keys)
            {
                writer.WriteLine(key + ": \"" + dictionary[key] + "\"");
            }

            writer.WriteLine("---");
        }

        public void Write(string text)
        {
            writer.WriteLine(text);
        }

        public void WriteHeading(string heading, int size)
        {
            if (size < 1 || size > 6) throw new ArgumentOutOfRangeException();

            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine(new string('#', size) + " " + heading);
            writer.WriteLine();
        }

        public void WriteNewLine()
        {
            writer.WriteLine();
        }

        public void WriteTableHeader(IEnumerable<string> headings)
        {
            writer.WriteLine();
            writer.Write("| ");

            foreach (var heading in headings)
            {
                writer.Write(" " + heading + " |");
            }

            writer.WriteLine();

            writer.Write("|");

            foreach (var heading in headings)
            {
                writer.Write(new string('-', heading.Length + 2) + "|");
            }

            writer.WriteLine();
        }

        public void WriteTableRow(params string[] cells)
        {
            writer.Write("| ");

            foreach (var cell in cells)
            {
                writer.Write(cell + " |");
            }

            writer.WriteLine();
        }

        public void WriteTableRow(string shortCode, IDocumentLink change, IReportOutput output, FileMap map)
        {
            bool outputDeadLinks = false;

            // look up correct path...
            IFolder folder = new PhysicalFolder(System.IO.Path.GetDirectoryName(output.Path));

            string fullPath = map.PathFor(change.Identifier);

            bool linkIsGood = System.IO.File.Exists(fullPath);

            if (linkIsGood || outputDeadLinks)
            {
                if (linkIsGood)
                {
                    string relativePath = map.PathRelativeTo(change.Identifier, folder);
                    string link = string.Format("[{0}]({1})", change.LinkText,
                        map.PathRelativeTo(change.Identifier, folder));
                    WriteTableRow(link);
                }
                else
                {
                    WriteTableRow(change.LinkText);
                }
            }
        }

        public void WriteTableRow(string shortCode, INameDescriptor change, IReportFormat format)
        {
            WriteTableRow(change.Name);
        }

        public void WriteTableRow(string shortCode, IValueDescriptor change, IReportFormat format)
        {
            string text = change.Value.ToString();

            ICoded code = change.Value as ICoded;

            if (code != null)
                text = format.Format(code);

            WriteTableRow(text);
        }

        public void WriteTableRow(string shortCode, INameValueDescriptor change, IReportFormat format)
        {
            string text = change.Value.ToString();

            ICoded code = change.Value as ICoded;

            if (code != null)
                text = format.Format(code);

            WriteTableRow(change.Name, text);
        }

        public void WriteTableRow(string shortCode, IDeltaDescriptor change, IReportFormat format)
        {
            string wasText = change.Was.ToString();
            string isText = change.IsNow.ToString();

            ICoded was = change.Was as ICoded;
            ICoded isNow = change.IsNow as ICoded;

            if (was != null)
            {
                wasText = format.Format(was);
            }

            if (isNow != null)
            {
                isText = format.Format(isNow);
            }

            WriteTableRow(wasText, isText);
        }

        public void WriteTableRow(string shortCode, INamedDeltaDescriptor change, IReportFormat format)
        {
            string wasText = change.Was.ToString();
            string isText = change.IsNow.ToString();

            ICoded was = change.Was as ICoded;
            ICoded isNow = change.IsNow as ICoded;

            if (was != null)
            {
                wasText = format.Format(was);
            }

            if (isNow != null)
            {
                isText = format.Format(isNow);
            }

            WriteTableRow(change.Name, wasText, isText);
        }

        public void WriteTableRow(string shortCode, ICodeDescriptor change, IReportFormat format)
        {
            string text = format.Format(change.Code);

            WriteTableRow(text);
        }

        public void WriteTableRowLink(string cell1, string cell2, string link)
        {
            WriteTableRow(new string[] { string.Format("[{0}]({1})", cell1, link), string.Format("[{0}]({1})", cell2, link) });
        }
        public void WriteTableRow(string cell1, int cell2)
        {
            WriteTableRow(cell1, cell2.ToString());
        }

        public void WriteTableRow(string cell1, int cell2, string link)
        {
            WriteTableRowLink(cell1, cell2.ToString(), link);
        }

        public void RenderLink(string href, string title, string anchorText)
        {
            writer.Write(string.Format("[{0}]({1}) ", anchorText, href));
        }

    }
}

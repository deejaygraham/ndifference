using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.IO;

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

        public void Write(string text)
        {
            writer.WriteLine(text);
        }

        public void WriteNewLine()
        {
            writer.WriteLine();
        }

        public void WriteTableRow(params string[] cells)
        {
            writer.Write("| ");

            foreach (var cell in cells)
            {
                writer.Write(cell + " | ");
            }

            writer.WriteLine();
        }

        // used
        public void WriteTableRow(IDocumentLink change, IReportOutput output, FileMap map)
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
                        map.PathRelativeTo(change.Identifier, folder).Replace(".md", ".html"));
                    WriteTableRow(link);
                }
                else
                {
                    WriteTableRow(change.LinkText);
                }
            }
        }

        //public void WriteTableRow(string shortCode, INameDescriptor change, IReportFormat format, string typeName = null, string assemblyName = null)
        //{
        //    WriteTableRow(change.Name, typeName, assemblyName);
        //}

        public void WriteTableRow(IValueDescriptor change, IReportFormat format, string typeName = null, string assemblyName = null)
        {
            string text = change.Value.ToString();

            ICoded code = change.Value as ICoded;

            if (code != null)
                text = format.Format(code);

            WriteTableRow(text, typeName, assemblyName);
        }

        public void WriteTableRow(INameValueDescriptor change, IReportFormat format, string typeName = null, string assemblyName = null)
        {
            string text = change.Value.ToString();

            ICoded code = change.Value as ICoded;

            if (code != null)
                text = format.Format(code);

            WriteTableRow(change.Name, text, typeName, assemblyName);
        }

        public void WriteTableRow(IDeltaDescriptor change, IReportFormat format, string typeName = null, string assemblyName = null)
        {
            WriteTableRow(change.Was, change.IsNow, typeName, assemblyName);
        }

        public void WriteTableRow(ICodeDeltaDescriptor change, IReportFormat format, string typeName = null, string assemblyName = null)
        {
            string wasText = format.Format(change.Was);
            string isText = format.Format(change.IsNow);

            WriteTableRow(wasText, isText, typeName, assemblyName);
        }

        public void WriteTableRow(INameDescriptor change, IReportFormat format, string typeName = null, string assemblyName = null)
        {
            WriteTableRow(change.Name, change.Reason, typeName, assemblyName);
        }

        public void WriteTableRow(INamedDeltaDescriptor change, IReportFormat format, string typeName = null, string assemblyName = null)
        {
            WriteTableRow(change.Name, change.Was, change.IsNow, typeName, assemblyName);
        }

        /// <summary>
        /// Write out a simple code change with a reason for the change.
        /// </summary>
        /// <param name="change"></param>
        /// <param name="format"></param>
        /// <param name="typeName"></param>
        /// <param name="assemblyName"></param>
        public void WriteTableRow(ICodeDescriptor change, IReportFormat format, string typeName = null, string assemblyName = null)
        {
            string code = format.Format(change.Code);

            WriteTableRow(code, change.Reason, assemblyName);
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

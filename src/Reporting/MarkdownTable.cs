using NDifference.Analysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NDifference.Reporting
{
    public class MarkdownTable
    {
        private List<string> columnNames = new List<string>();

        private List<string> rows = new List<string>();

        public MarkdownTable()
        {
        }

        public MarkdownTable(IEnumerable<string> headings)
        {
            this.columnNames = new List<string>(headings);
        }

        public void AddColumn(string name)
        {
            if (!columnNames.Contains(name))
                this.columnNames.Add(name);
        }

        public void AddColumns(IEnumerable<string> names)
        {
            Debug.Assert(names.Any(), "No column names specified");
            this.columnNames.AddRange(names);
        }

        public void AddRow(IdentifiedChange change)
        {
            Debug.Assert(change != null, "Change not set");
            Debug.Assert(change.Descriptor != null, "Change not set");

            AddRow((dynamic)change.Descriptor, change);
        }

        private void AddRow(INameDescriptor descriptor, IdentifiedChange change)
        {
            var row = new List<string>();
            row.Add(descriptor.Name);

            if (columnNames.Contains("Reason") && !string.IsNullOrEmpty(descriptor.Reason))
                row.Add(descriptor.Reason);

            if (columnNames.Contains("Type"))
                row.Add(change.TypeName);

            if (columnNames.Contains("Assembly"))
                row.Add(change.AssemblyName);

            AddRow(row.ToArray());
        }

        //private void AddRow(IValueDescriptor descriptor, IdentifiedChange change)
        //{
        //    var row = new List<string>();
        //    row.Add(descriptor.Value.ToString());

        //    if (columnNames.Contains("Reason"))
        //        row.Add(descriptor.Reason);

        //    if (columnNames.Contains("Type"))
        //        row.Add(change.TypeName);

        //    if (columnNames.Contains("Assembly"))
        //        row.Add(change.AssemblyName);

        //    AddRow(row.ToArray());
        //}

        private void AddRow(INameValueDescriptor descriptor, IdentifiedChange change)
        {
            var row = new List<string>();
            row.Add(descriptor.Name);

            if (descriptor.Value != null)
                row.Add(descriptor.Value.ToString());

            if (columnNames.Contains("Reason"))
                row.Add(descriptor.Reason);

            if (columnNames.Contains("Type"))
                row.Add(change.TypeName);

            if (columnNames.Contains("Assembly"))
                row.Add(change.AssemblyName);

            AddRow(row.ToArray());
        }

        private void AddRow(IDeltaDescriptor descriptor, IdentifiedChange change)
        {
            var row = new List<string>();
            row.Add(descriptor.Was);
            row.Add(descriptor.IsNow);

            if (columnNames.Contains("Reason"))
                row.Add(descriptor.Reason);

            if (columnNames.Contains("Type"))
                row.Add(change.TypeName);

            if (columnNames.Contains("Assembly"))
                row.Add(change.AssemblyName);

            AddRow(row.ToArray());
        }

        private void AddRow(ICodeSignature descriptor, IdentifiedChange change)
        {
            var row = new List<string>();
            row.Add(descriptor.Signature.ToPlainText());

            if (columnNames.Contains("Reason") && !string.IsNullOrEmpty(descriptor.Reason))
                row.Add(descriptor.Reason);

            if (columnNames.Contains("Type"))
                row.Add(change.TypeName);

            if (columnNames.Contains("Assembly"))
                row.Add(change.AssemblyName);

            AddRow(row.ToArray());
        }

        private void AddRow(IChangedCodeSignature descriptor, IdentifiedChange change)
        {
            var row = new List<string>();

            if (columnNames.Contains("Was") && descriptor.Was != null)
                row.Add(descriptor.Was.ToPlainText());

            if (columnNames.Contains("Is Now") && descriptor.IsNow != null)
                row.Add(descriptor.IsNow.ToPlainText());

            if (columnNames.Contains("Reason") && !string.IsNullOrEmpty(descriptor.Reason))
                row.Add(descriptor.Reason);

            if (columnNames.Contains("Type"))
                row.Add(change.TypeName);

            if (columnNames.Contains("Assembly"))
                row.Add(change.AssemblyName);

            AddRow(row.ToArray());
        }


        private void AddRow(INamedDeltaDescriptor descriptor, IdentifiedChange change)
        {
            var row = new List<string>();
            row.Add(descriptor.Name);

            row.Add(descriptor.Was);
            row.Add(descriptor.IsNow);

            if (columnNames.Contains("Reason") && !string.IsNullOrEmpty(descriptor.Reason))
                row.Add(descriptor.Reason);

            if (columnNames.Contains("Type"))
                row.Add(change.TypeName);

            if (columnNames.Contains("Assembly"))
                row.Add(change.AssemblyName);

            AddRow(row.ToArray());
        }

        public void AddRow(params string[] cells)
        {
            string row = "| " + String.Join(" | ", cells) + " |";
            this.rows.Add(row);

            bool debugRowCounts = false;

            if (debugRowCounts)
            {
                int rowCellCount = cells.Length;
                int tableHeadingCount = columnNames.Count;

                if (rowCellCount != tableHeadingCount)
                {
                    StringBuilder message = new StringBuilder();
                    message.AppendFormat("Row incorrectly formatted: Expecting {0} got {1}\n", tableHeadingCount,
                        rowCellCount);
                    message.AppendLine(FormatTableRow(columnNames));

                    Debug.Assert(rowCellCount == tableHeadingCount, message.ToString());
                }
            }
        }

        private string FormatTableRow(IEnumerable<string> cells)
        {
            return "| " + String.Join(" | ", cells) + " |";
        }

        private string FormatHeadingDivider(IEnumerable<string> cells)
        {
            var builder = new StringBuilder();

            // write underlines for headings
            builder.Append("|");

            foreach (var heading in columnNames)
            {
                builder.Append(new string('-', heading.Length + 2) + "|");
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(FormatTableRow(columnNames));

            // write underlines for headings
            builder.AppendLine(FormatHeadingDivider(columnNames));

            // write out content...
            foreach (string row in rows)
            {
                builder.AppendLine(row);
            }

            return builder.ToString();
        }
    }

    public class MarkdownTableHeader
    {
        private readonly List<string> columnNames = new List<string>();

        public MarkdownTableHeader()
        {
        }

        public MarkdownTableHeader(IEnumerable<string> headings)
        {
            this.columnNames = new List<string>(headings);
        }

        public void AddColumn(string name)
        {
            if (!columnNames.Contains(name))
                this.columnNames.Add(name);
        }

        public void AddColumns(IEnumerable<string> names)
        {
            this.columnNames.AddRange(names);
        }
        
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(FormatTableRow(columnNames));

            // write underlines for headings
            builder.AppendLine(FormatHeadingDivider(columnNames));

            return builder.ToString();
        }

        private string FormatTableRow(IEnumerable<string> cells)
        {
            return "| " + String.Join(" | ", cells) + " |";
        }

        private string FormatHeadingDivider(IEnumerable<string> cells)
        {
            var builder = new StringBuilder();

            // write underlines for headings
            builder.Append("|");

            foreach (var heading in columnNames)
            {
                builder.Append(new string('-', heading.Length + 2) + "|");
            }

            return builder.ToString();
        }

    }

    public class MarkdownTableRow
    {
        public override string ToString()
        {
            var builder = new StringBuilder();

            // not yet
            return builder.ToString();
        }

        private string FormatTableRow(IEnumerable<string> cells)
        {
            return "| " + String.Join(" | ", cells) + " |";
        }
    }

}

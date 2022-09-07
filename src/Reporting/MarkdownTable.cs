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

        public bool Consolidated { get; set; }

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

            AddRow((dynamic) change.Descriptor, change);
        }

        private void AddRow(INameDescriptor descriptor, IdentifiedChange change)
        {
            AddRow(descriptor.Name);
        }

        private void AddRow(IValueDescriptor descriptor, IdentifiedChange change)
        {
            AddRow(descriptor.Value.ToString());
        }

        private void AddRow(INameValueDescriptor descriptor, IdentifiedChange change)
        {
            var row = new List<string>();
            row.Add(descriptor.Name);
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

        private void AddRow(ICodeDescriptor descriptor, IdentifiedChange change)
        {
            var row = new List<string>();
            row.Add(descriptor.Code.ToPlainText());

            if (columnNames.Contains("Reason"))
                row.Add(descriptor.Reason);

            if (columnNames.Contains("Type"))
                row.Add(change.TypeName);

            if (columnNames.Contains("Assembly"))
                row.Add(change.AssemblyName);

            AddRow(row.ToArray());
        }

        private void AddRow(ICodeDeltaDescriptor descriptor, IdentifiedChange change)
        {
            var row = new List<string>();
            row.Add(descriptor.Was.ToPlainText());
            row.Add(descriptor.IsNow.ToPlainText());

            if (columnNames.Contains("Reason"))
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

            if (columnNames.Contains("Reason"))
                row.Add(descriptor.Reason);

            if (columnNames.Contains("Type"))
                row.Add(change.TypeName);

            if (columnNames.Contains("Assembly"))
                row.Add(change.AssemblyName);

            AddRow(row.ToArray());
        }

        public void AddRow(params string[] cells)
        {
            int cellCount = cells.Length;
            int headingCount = columnNames.Count;

            Debug.Assert(cellCount == headingCount, "Table incorrectly formatted");

            this.rows.Add(String.Join(" | ", cells));
        }


        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("|");

            foreach (var heading in columnNames)
            {
                builder.Append(" " + heading + " |");
            }

            builder.AppendLine();

            // write underlines for headings
            builder.Append("|");

            foreach (var heading in columnNames)
            {
                builder.Append(new string('-', heading.Length + 2) + "|");
            }

            builder.AppendLine();

            // write out content...
            foreach (string row in rows)
            {
                builder.AppendLine(row);
            }

            return builder.ToString();
        }
    }
}

using NDifference.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using NDifference.Inspection;

namespace NDifference.Reporting
{
    public class MarkdownReportWriter : IReportWriter
    {
        private readonly ReportAsMarkdown _format = new ReportAsMarkdown();

        public IEnumerable<IReportFormat> SupportedFormats
        {
            get
            {
                return new IReportFormat[] { _format };
            }
        }

        public FileMap Map { get; set; }

		public void Write(IdentifiedChangeCollection changes, IReportOutput output, IReportFormat format)
        {
			Debug.Assert(changes != null, "changes object must be set");
			Debug.Assert(output != null, "output object must be set");

			TextWriter text = new StringWriter();

			try
			{
				if (this.Map != null)
				{
					string folder = Path.GetDirectoryName(output.Path);

					if (!Directory.Exists(folder))
					{
						Directory.CreateDirectory(folder);
					}
				}

				Encoding utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

                using (MarkdownWriter mdw = MarkdownWriter.Create(text))
                {
                    var metadata = new Dictionary<string, string>
                    {
                        { "title", changes.Heading },
                        { "id", changes.Identifier },
                        { "layout", "layouts/overview.njk" }
                    };

                    mdw.Write("---");

                    foreach (var key in metadata.Keys)
                    {
                        mdw.Write(key + ": \"" + metadata[key] + "\"");
                    }

                    mdw.Write("---");

                    mdw.Write(format.FormatComment("markdownlint-disable-file"));

                    if (!String.IsNullOrEmpty(changes.HeadingBlock))
                    {
                        mdw.Write(changes.HeadingBlock);
                    }

                    // content.Add("Summary Table");
                    mdw.WriteNewLine();
                    mdw.WriteNewLine();
                    mdw.Write(format.FormatTitle(2, "Summary of changes", null));
                    mdw.WriteNewLine();

                    var summaryTable = new MarkdownTable(new string[] { "Item", "Value" });

					foreach (var key in changes.SummaryBlocks.Keys)
                    {
                        summaryTable.AddRow(new string[] { key, changes.SummaryBlocks[key] });
                        //mdw.Write(format.FormatTableRow(new string[] { key, changes.SummaryBlocks[key] }));
                    }

                    CategoryRegistry registry = new CategoryRegistry();

                    var groupedChanges = changes.Changes.GroupBy(x => x.Priority).Reverse();

                    foreach (var groupChange in groupedChanges)
                    {
                        var category = registry.ForPriority(groupChange.Key);

                        string changeLink = format.FormatLink("#" + category.Identifier, category.Name);
                        string countLink = format.FormatLink("#" + category.Identifier, groupChange.Count().ToString());

                        summaryTable.AddRow(new string[] { changeLink, countLink });
                    }

                    mdw.Write(summaryTable.ToString());

                    foreach (var priorityGroup in groupedChanges)
                    {
                        var category = registry.ForPriority(priorityGroup.Key);

                        mdw.WriteNewLine();
                        mdw.WriteNewLine();
                        mdw.Write(format.FormatTitle(2, category.Name, category.Identifier));
                        mdw.WriteNewLine();

                        //mdw.Write(category.FullDescription);

                        var table = new MarkdownTable(WellKnownChangePriorities.ColumnNames(priorityGroup.First().Priority, changes.Consolidated));

                        foreach (var change in priorityGroup)
                        {
                            if (change.Descriptor is IDocumentLink)
                            {
                                IDocumentLink descriptor = change.Descriptor as IDocumentLink;

                                // look up correct path...
                                IFolder folder = new PhysicalFolder(System.IO.Path.GetDirectoryName(output.Path));

                                string relativePath = this.Map.PathRelativeTo(descriptor.Identifier, folder);
                                string link = format.FormatLink(relativePath.Replace(".md", ".html").Replace(" ", ""), descriptor.LinkText);
                                table.AddRow(link);
                            }
                            else
                            {
                                table.AddRow(change);
                            }
                        }

                        mdw.Write(table.ToString());

                        mdw.WriteNewLine();
                    }
                }

                string content = text.ToString();

                output.Execute(content);
            }
			finally
			{
				if (text != null)
				{
					text.Dispose();
					text = null;
				}
			}
		}
    }
}

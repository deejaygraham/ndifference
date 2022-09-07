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

                    var summaryTable = new MarkdownTable();

                    summaryTable.AddColumns(new string[] { "Item", "Value" });
//                    mdw.Write(format.FormatTableHeader(new string[] { "Item", "Value" }));

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

                        var headings = ColumnNames(priorityGroup.First());

                        var table = new MarkdownTable();
                        Debug.Assert(headings != null, "Headings not set correctly for priority type");

                        table.AddColumns(headings);

                        if (changes.Consolidated)
                        {
                            table.Consolidated = true;
                            table.AddColumn("Type");
                            table.AddColumn("Assembly");
                        }

                        foreach (var change in priorityGroup)
                        {
                            if (change.Descriptor is IDocumentLink)
                            {
                                IDocumentLink descriptor = change.Descriptor as IDocumentLink;

                                // look up correct path...
                                IFolder folder = new PhysicalFolder(System.IO.Path.GetDirectoryName(output.Path));

                                string relativePath = this.Map.PathRelativeTo(descriptor.Identifier, folder);
                                string link = format.FormatLink(relativePath.Replace(".md", ".html"), descriptor.LinkText);
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

        private IEnumerable<string> ColumnNames(IdentifiedChange change)
        {
            var columns = new List<string>();

            if (change.Priority == WellKnownChangePriorities.RemovedAssemblies 
                     || change.Priority == WellKnownChangePriorities.AddedAssemblies
                     || change.Priority == WellKnownChangePriorities.ChangedAssemblies)
            {
                // single column
                columns.Add("Assembly");
            }
            else if (change.Priority == WellKnownChangePriorities.BreakingChanges
                    || change.Priority == WellKnownChangePriorities.PotentiallyChangedAssemblies) // not used
            {
                columns.Add("Breaking Changes");
            }
            else if (change.Priority == WellKnownChangePriorities.AssemblyInternal
                    || change.Priority == WellKnownChangePriorities.TypeInternal)
            {
                // class structure has changed
                columns.Add("Was");
                columns.Add("Is Now");
                columns.Add("Reason");
            }
            else if (change.Priority == WellKnownChangePriorities.RemovedReferences
                     || change.Priority == WellKnownChangePriorities.AddedReferences)
            {
                columns.Add("Reference");
            }
            else if (change.Priority == WellKnownChangePriorities.AddedTypes 
                     || change.Priority == WellKnownChangePriorities.ChangedTypes
                     || change.Priority == WellKnownChangePriorities.RemovedTypes)
            {
                columns.Add("Type");
            }
            else if (change.Priority == WellKnownChangePriorities.ObsoleteTypes)
            {
                columns.Add("Type");
                columns.Add("Message");
            }
            else if (change.Priority == WellKnownChangePriorities.PotentiallyChangedTypes) // not used
            {
                columns.Add("Potentially Changed Types");
            }
            else if (change.Priority == WellKnownChangePriorities.ConstantsAdded
                    || change.Priority == WellKnownChangePriorities.ConstantsRemoved
                    || change.Priority == WellKnownChangePriorities.ConstructorsAdded
                    || change.Priority == WellKnownChangePriorities.ConstructorsRemoved
                    || change.Priority == WellKnownChangePriorities.DelegatesAdded
                    || change.Priority == WellKnownChangePriorities.DelegatesRemoved
                    || change.Priority == WellKnownChangePriorities.EnumValuesAdded
                    || change.Priority == WellKnownChangePriorities.EnumValuesRemoved
                    || change.Priority == WellKnownChangePriorities.EventsAdded
                    || change.Priority == WellKnownChangePriorities.EventsRemoved
                    || change.Priority == WellKnownChangePriorities.FinalizersAdded
                    || change.Priority == WellKnownChangePriorities.FinalizersRemoved
                    || change.Priority == WellKnownChangePriorities.FieldsAdded
                    || change.Priority == WellKnownChangePriorities.FieldsRemoved
                    || change.Priority == WellKnownChangePriorities.IndexersAdded
                    || change.Priority == WellKnownChangePriorities.IndexersRemoved
                    || change.Priority == WellKnownChangePriorities.MethodsAdded
                    || change.Priority == WellKnownChangePriorities.MethodsRemoved
                    || change.Priority == WellKnownChangePriorities.PropertiesAdded
                    || change.Priority == WellKnownChangePriorities.PropertiesRemoved)
            {
                columns.Add("Signature");
            }
            else if (change.Priority == WellKnownChangePriorities.ConstantsObsolete
                    || change.Priority == WellKnownChangePriorities.ConstructorsObsolete
                    || change.Priority == WellKnownChangePriorities.DelegatesObsolete
                    || change.Priority == WellKnownChangePriorities.EventsObsolete
                    || change.Priority == WellKnownChangePriorities.FieldsObsolete
                    || change.Priority == WellKnownChangePriorities.FinalizersObsolete
                    || change.Priority == WellKnownChangePriorities.IndexersObsolete
                    || change.Priority == WellKnownChangePriorities.MethodsObsolete
                    || change.Priority == WellKnownChangePriorities.PropertiesObsolete)
            {
                columns.Add("Signature");
                columns.Add("Reason");
            }
            else if (change.Priority == WellKnownChangePriorities.ConstantsChanged
                    || change.Priority == WellKnownChangePriorities.FieldsChanged
                    || change.Priority == WellKnownChangePriorities.ConstructorsChanged
                    || change.Priority == WellKnownChangePriorities.DelegatesChanged
                    || change.Priority == WellKnownChangePriorities.EnumValuesChanged
                    || change.Priority == WellKnownChangePriorities.EventsChanged
                    || change.Priority == WellKnownChangePriorities.FinalizersChanged
                    || change.Priority == WellKnownChangePriorities.IndexersChanged
                    || change.Priority == WellKnownChangePriorities.MethodsChanged
                    || change.Priority == WellKnownChangePriorities.PropertiesChanged)
            {
                columns.Add("Was");
                columns.Add("Is Now");
            }
            else if (change.Priority == WellKnownChangePriorities.TypeDebug)
            {
                // nothing
                columns.Add("Debug");
            }
            else
            {
                columns.Add("Priority Not Handled!");
            }

            return columns;
        }
    }
}

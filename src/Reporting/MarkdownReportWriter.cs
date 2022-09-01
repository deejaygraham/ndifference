using NDifference.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

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

		public void Write(IdentifiedChangeCollection changes, IReportOutput output, IReportFormat format, bool consolidateReport)
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

                    mdw.Write(format.FormatTableHeader(new string[] { "Item", "Value" }));

					foreach (var key in changes.SummaryBlocks.Keys)
                    {
                        mdw.Write(format.FormatTableRow(new string[] { key, changes.SummaryBlocks[key] }));
                    }

                    CategoryRegistry registry = new CategoryRegistry();

                    var groupedChanges = changes.Changes.GroupBy(x => x.Priority).Reverse();

                    foreach (var groupChange in groupedChanges)
                    {
                        var category = registry.ForPriority(groupChange.Key);

                        if (consolidateReport)
                        {
                            // does category include assembly and typename for consolidated report?
                            if (!category.Headings.Contains("Assembly"))
                            {
                                var newHeadings = new List<string>(category.Headings);
                                newHeadings.Add("Type");
                                newHeadings.Add("Assembly");

                                category.Headings = newHeadings.ToArray();
                            }
                        }

                        string changeLink = format.FormatLink("#" + category.Identifier, category.Name);
                        string countLink = format.FormatLink("#" + category.Identifier, groupChange.Count().ToString());

                        mdw.Write(format.FormatTableRow(new string[] { changeLink, countLink }));
                    }

                    foreach (var priorityGroup in groupedChanges)
                    {
                        var category = registry.ForPriority(priorityGroup.Key);

                        mdw.WriteNewLine();
                        mdw.WriteNewLine();
                        mdw.Write(format.FormatTitle(2, category.Name, category.Identifier));
                        mdw.WriteNewLine();

                        //mdw.Write(category.FullDescription);

                        if (category.Headings != null && category.Headings.Length > 0)
                        {
                            mdw.Write(format.FormatTableHeader(category.Headings));
                        }
                       
                        foreach (var change in priorityGroup)
                        {
                            RenderChange(change, mdw, output);
                        }

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

		private void RenderChange(IdentifiedChange change, MarkdownWriter mdw, IReportOutput output)
		{
			object descriptor = change.Descriptor;

			if (descriptor != null)
			{
				RenderDescriptor(change, descriptor, mdw, output);
			}
		}

        //private void Render(IDocumentLink link, MarkdownWriter mdw, IReportFormat format, IReportOutput output, FileMap map)
        //{
        //    if (link != null)
        //    {
        //        if (map != null)
        //        {
        //            //mdw.Write(fomatlink, output, this.Map);
        //        }
        //    }
        //}

        private void RenderDescriptor(IdentifiedChange change, object descriptor, MarkdownWriter mdw, IReportOutput output)
        {
            IDocumentLink link = descriptor as IDocumentLink;

            if (link != null)
            {
                if (this.Map != null)
                {
                    mdw.WriteTableRow(link, output, this.Map);
                }
            }
            else
            {
                ICodeDescriptor code = descriptor as ICodeDescriptor;

                if (code != null)
                {
                    string signature = this._format.Format(code.Code);

                    if (signature.Contains("&lt;") || code.Code.ToPlainText().Contains("&lt;"))
                    {

                    }

                    mdw.WriteTableRow(signature, code.Reason);
//                    mdw.WriteTableRow(code, this._format, change.TypeName, change.AssemblyName);
                }
                else
                {
                    INameValueDescriptor nvd = descriptor as INameValueDescriptor;

                    if (nvd != null)
                    {
                        mdw.WriteTableRow(nvd, this._format, change.TypeName, change.AssemblyName);
                    }
                    else
                    {
                        INamedDeltaDescriptor nd = descriptor as INamedDeltaDescriptor;

                        if (nd != null)
                        {
                            mdw.WriteTableRow(nd, this._format, change.TypeName, change.AssemblyName);
                        }
                        else
                        {
                            IValueDescriptor vd = descriptor as IValueDescriptor;

                            if (vd != null)
                            {
                                mdw.WriteTableRow(vd, this._format, change.TypeName, change.AssemblyName);
                            }
                            else
                            {
                                INamedDeltaDescriptor ndd = descriptor as INamedDeltaDescriptor;

                                if (ndd != null)
                                {
                                    mdw.WriteTableRow(ndd, this._format);
                                }
                                else
                                {
                                    IDeltaDescriptor delta = descriptor as IDeltaDescriptor;

                                    if (delta != null)
                                    {
                                        mdw.WriteTableRow(delta, this._format, change.TypeName, change.AssemblyName);
                                    }
                                    else
                                    {
                                        ICodeDeltaDescriptor codedelta = descriptor as ICodeDeltaDescriptor;

                                        if (codedelta != null)
                                        {
                                            mdw.WriteTableRow(codedelta, this._format, change.TypeName, change.AssemblyName);
                                        }
                                        else
                                        {
                                            INameDescriptor named = descriptor as INameDescriptor;

                                            if (named != null)
                                            {
                                                mdw.WriteTableRow(named, this._format, change.TypeName, change.AssemblyName);
                                            }
                                            else
                                            {
                                                Debug.Assert(false, "No Descriptor for this");
                                            }
                                        }
                                    }
                                    //else
                                    //{
                                    //	INameValueDescriptor textDesc = descriptor as INameValueDescriptor;

                                    //	if (textDesc != null)
                                    //	{
                                    //		mdw.WriteTableRow(change.Inspector, textDesc, this._format);
                                    //	}
                                    //}
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

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

                    mdw.WriteFrontMatter(metadata);
                    mdw.Write("<!-- markdownlint-disable-file -->");

                    if (!String.IsNullOrEmpty(changes.HeadingBlock))
                    {
                        mdw.Write(changes.HeadingBlock);
                    }

                    // content.Add("Summary Table");
                    mdw.WriteHeading("Summary of changes", 2);

                    mdw.WriteTableHeader(new string[] { "Item", "Value" });

					foreach (var key in changes.SummaryBlocks.Keys)
                    {
                        mdw.WriteTableRow(new string[] { key, changes.SummaryBlocks[key] });
                    }

                    // write each category
                    foreach (var cat in changes.Categories.OrderBy(x => x.Priority.Value))
                    {
                        var list = changes.ChangesInCategory(cat.Name);

                        if (list.Any())
                        {
							// write out links to each item in the page below:
                            string href = "#" + cat.Identifier;
                            string categoryLink = string.Format("[{0}]({1})", cat.Name, href);
                            string occurrenceLink = string.Format("[{0}]({1})", list.Count, href);

							mdw.WriteTableRow(new string[] { categoryLink, occurrenceLink });
						}
					}

                    foreach (var cat in changes.Categories.OrderBy(x => x.Priority.Value))
                    {
                        var list = changes.ChangesInCategory(cat.Name);

                        if (list.Any())
                            RenderCategory(cat, list, mdw, output);
                    }

                    var uncatChanges = changes.UnCategorisedChanges();

                    if (uncatChanges.Any())
                    {
                        var uncat = new Category
                        {
                            Priority = new CategoryPriority(999), Description = "Uncategorised changes",
                            Name = "Uncategorised Changes"
                        };

                        RenderCategory(uncat, uncatChanges, mdw, output);
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
		private void RenderCategory(Category cat, IEnumerable<IdentifiedChange> changes, MarkdownWriter mdw, IReportOutput output)
		{
			mdw.WriteNewLine();
	
            if (changes.Any())
			{
				mdw.WriteHeading(cat.Name, 2);

				mdw.Write(cat.FullDescription);

                if (cat.Headings != null && cat.Headings.Length > 0)
                {
                    mdw.WriteTableHeader(cat.Headings);
                }

                // order changes...
                var ordered = new List<IdentifiedChange>(changes);
                ordered.Sort(new IdentifiedChangeComparer());

                foreach (var change in ordered)
                {
                    RenderChange(change, mdw, output);
                }

                mdw.WriteNewLine();
            }

            mdw.WriteNewLine();
		}

		private void RenderChange(IdentifiedChange change, MarkdownWriter mdw, IReportOutput output)
		{
			object descriptor = change.Descriptor;

			if (descriptor != null)
			{
				RenderDescriptor(change, descriptor, mdw, output);
			}
			else if (!String.IsNullOrEmpty(change.Description))
			{
				mdw.WriteTableRow(/*change.Inspector, */new string[] { change.Description });
			}
		}

        private void RenderDescriptor(IdentifiedChange change, object descriptor, MarkdownWriter mdw, IReportOutput output)
        {
            IDocumentLink link = descriptor as IDocumentLink;

            if (link != null)
            {
                if (this.Map != null)
                {
                    mdw.WriteTableRow(change.Inspector, link, output, this.Map);
                }
            }
            else
            {
                ICodeDescriptor code = descriptor as ICodeDescriptor;

                if (code != null)
                {
                    mdw.WriteTableRow(change.Inspector, code, this._format);
                }
                else
                {
                    INameValueDescriptor nvd = descriptor as INameValueDescriptor;

                    if (nvd != null)
                    {
                        mdw.WriteTableRow(change.Inspector, nvd, this._format);
                    }
                    else
                    {
                        INamedDeltaDescriptor nd = descriptor as INamedDeltaDescriptor;

                        if (nd != null)
                        {
                            mdw.WriteTableRow(change.Inspector, nd, this._format);
                        }
                        else
                        {
                            IValueDescriptor vd = descriptor as IValueDescriptor;

                            if (vd != null)
                            {
                                mdw.WriteTableRow(change.Inspector, vd, this._format);
                            }
                            else
                            {
                                INamedDeltaDescriptor ndd = descriptor as INamedDeltaDescriptor;

                                if (ndd != null)
                                {
                                    mdw.WriteTableRow(change.Inspector, ndd, this._format);
                                }
                                else
                                {
                                    IDeltaDescriptor delta = descriptor as IDeltaDescriptor;

                                    if (delta != null)
                                    {
                                        mdw.WriteTableRow(change.Inspector, delta, this._format);
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

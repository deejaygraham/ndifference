using NDifference.Analysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace NDifference.Reporting
{
	public class HtmlReportWriter : IReportWriter
	{
		private ReportAsHtml4 _format = new ReportAsHtml4();

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
					//output.Path = this.Map.Lookup(changes.Identifier);

					string folder = Path.GetDirectoryName(output.Path);

					if (!Directory.Exists(folder))
					{
						Directory.CreateDirectory(folder);
					}
				}

				var settings = new XmlWriterSettings
				{
					Encoding = Encoding.UTF8,
					OmitXmlDeclaration = true,
					Indent = true,
					IndentChars = "\t"
				};

				using (XmlWriter html = XmlTextWriter.Create(text, settings))
				{
					html.WriteStartDocument();

					html.WriteRaw("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">\r\n");

					html.WriteElement("html", () =>
					{
						html.WriteElement("head", () =>
						{
							html.WriteRaw("\r\n<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\" >\r\n");

							if (!String.IsNullOrEmpty(changes.Heading))
							{
								html.WriteElement("title", () =>
								{
									html.WriteString(changes.Heading);
								});
							}

							html.WriteWhitespace("\r\n");
							html.WriteComment(" " + changes.Identifier + " ");
							html.WriteWhitespace("\r\n");

							if (changes.MetaBlocks != null)
							{
								foreach (var meta in changes.MetaBlocks)
								{
									html.WriteRaw(meta);
									html.WriteWhitespace("\r\n");
								}
							}
						});

						html.WriteElement("body", () =>
						{
							if (!String.IsNullOrEmpty(changes.Heading))
							{
								html.WriteElement("div", () =>
								{
									html.WriteAttributeString("id", "header");
									html.WriteElement("h1", () =>
									{
										html.WriteString(changes.Heading);
									});

									if (changes.Parents.Any())
									{
										string currentFolder = Path.GetDirectoryName(output.Path);

										// do breadcrumbs...
										html.WriteElement("ul", () =>
										{
											changes.Parents.ForEach(x =>
											{
												html.WriteElement("li", () =>
												{
													html.WriteLink(this.Map.PathRelativeTo(x.Identifier, new PhysicalFolder(currentFolder)), x.LinkText);
												});
											});
										});
									}
								});
							}

							if (!String.IsNullOrEmpty(changes.HeadingBlock))
							{
								html.WriteElement("div", () =>
								{
									html.WriteAttributeString("id", "exp");
									html.WriteString(changes.HeadingBlock);
								});
							}

							html.WriteElement("div", () =>
							{
								html.WriteAttributeString("id", "summary");
								html.WriteElement("h2", () =>
								{
									html.WriteString(changes.Name);
								});

								html.WriteComment(" Summary Table ");

								html.WriteElement("table", () =>
								{
									html.WriteAttributeString("summary", "Summary of changes between versions");

									html.WriteElement("caption", () =>
									{
										html.WriteString("Summary of changes");
									});

									html.WriteElement("tbody", () =>
									{
										foreach (var key in changes.SummaryBlocks.Keys)
										{
											html.WriteTableRow(key, changes.SummaryBlocks[key]);
										}

										// write each category
										foreach (var cat in changes.Categories.OrderBy(x => x.Priority.Value))
										{
											if (changes.ChangesInCategory(cat.Priority.Value).Any())
											{
												html.WriteTableRow(cat.Name, changes.ChangesInCategory(cat.Priority.Value).Count, "#" + cat.Identifier);
											}
										}
									});
								});

								html.WriteComment(" End of Summary Table ");
							});

							foreach (var cat in changes.Categories.OrderBy(x => x.Priority.Value))
							{
								RenderCategory(cat, changes.ChangesInCategory(cat.Priority.Value), html, output);
							}

							var uncatChanges = changes.UnCategorisedChanges();

							if (uncatChanges.Any())
							{
								html.WriteComment(" Writing Uncategorised changes ... ");

								var uncat = new Category { Priority = new CategoryPriority(999), Description = "Uncategorised changes", Name = "Uncategorised Changes" };

								RenderCategory(uncat, uncatChanges, html, output);
							}

							foreach (var footer in changes.FooterBlocks)
							{
								html.WriteWhitespace("\r\n");
								html.WriteRaw(footer);
								html.WriteWhitespace("\r\n");
							}
						});
					});

					html.WriteEndDocument();
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

		private void RenderCategory(Category cat, IEnumerable<IdentifiedChange> changes, XmlWriter html, IReportOutput output)
		{
			html.WriteElement("div", () =>
			{
				html.WriteAttributeString("id", cat.Identifier.ToString());

				if (changes.Any())
				{
					html.WriteElement("h2", () =>
					{
						html.WriteString(cat.Name);
					});

					html.WriteComment("Category P" + cat.Priority.Value);

					html.WriteElement("table", () =>
					{
						if (!String.IsNullOrEmpty(cat.Description))
						{
							html.WriteAttributeString("summary", cat.Description);

							html.WriteElement("caption", () =>
							{
								// new property on category - caption ?
								html.WriteString(cat.Description);
							});
						}


						html.WriteElement("tbody", () =>
						{
							foreach (var change in changes)
							{
								html.WriteElement("tr", () =>
								{
									object descriptor = change.Descriptor;

									if (descriptor != null)
									{
										IDocumentLink link = descriptor as IDocumentLink;

										if (link != null && this.Map != null)
										{
											html.WriteElement("td", () =>
											{
												// look up correct path...
												IFolder folder = new PhysicalFolder(System.IO.Path.GetDirectoryName(output.Path));

												html.WriteLink(this.Map.PathRelativeTo(link.Identifier, folder), link.LinkText + " " + change.Inspector);
											});
										}
										else
										{
											ITextDescriptor textDesc = descriptor as ITextDescriptor;

											if (textDesc != null)
											{
												html.WriteTableRow(textDesc.Name, textDesc.Message + " " + change.Inspector);
											}
										}
									}
									else if (!String.IsNullOrEmpty(change.Description))
									{
										html.WriteTableRow(change.Description + " " + change.Inspector);
									}
								});
							}
						});
					});
				}
				else
				{
					html.WriteComment(" No " + cat.Name + " identified ");
				}
			});
		}
	}
}

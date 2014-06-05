using NDifference.Analysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public OutputFileMap Map { get; set; }

		public void Write(IdentifiedChangeCollection changes, IReportOutput output, IReportFormat format)
		{
			Debug.Assert(changes != null, "changes object must be set");
			Debug.Assert(output != null, "output object must be set");

			TextWriter text = new StringWriter();

			try
			{
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
										foreach (var cat in changes.Categories)
										{
											if (changes.ChangesInCategory(cat.Priority).Any())
											{
												html.WriteTableRow(cat.Name, changes.ChangesInCategory(cat.Priority).Count, "#" + cat.Identifier);
											}
										}
									});
								});

								html.WriteComment(" End of Summary Table ");
							});

							foreach (var cat in changes.Categories)
							{
								// write table for each

								//		html.WriteElement("table", () =>
								//		{
								//			html.WriteAttributeString("summary", "List of assemblies that have changed between versions");

								//			html.WriteElement("caption", () =>
								//			{
								//				html.WriteString("List of changed assemblies");
								//			});

								//			html.WriteElement("tbody", () =>
								//			{
								//				foreach (var change in summaryChanges.Changes)
								//				{
								//					html.WriteElement("tr", () =>
								//					{
								//						html.WriteElement("td", () =>
								//						{
								//							html.WriteLink(fileMap.LookupRelativeTo(change.Second.Identifier, project.Settings.IndexPath), change.Second.Name);
								//						});
								//					});
								//				}
								//			});
								//		});

								html.WriteElement("div", () =>
								{
									html.WriteAttributeString("id", cat.Identifier.ToString());
									html.WriteElement("h2", () =>
									{
										html.WriteString(cat.Name);
									});

									int inThisCat = 0;

									foreach (var change in changes.ChangesInCategory(cat.Priority))
									{
										++inThisCat;
										object descriptor = change.Descriptor;

										if (descriptor != null)
										{
											IDocumentLink link = descriptor as IDocumentLink;

											if (link != null)
											{
												html.WriteElement("p", () =>
												{
													// look up correct path...
													html.WriteLink(this.Map.LookupRelative(link.Identifier), link.LinkText);
												});
											}
										}
										else if (!String.IsNullOrEmpty(change.Description))
										{
											html.WriteElement("p", () =>
											{
												html.WriteString(change.Description);
											});
										}
									}

									if (inThisCat == 0)
									{
										html.WriteComment(" No " + cat.Name + " identified ");
									}
								});
							}

							if (changes.UnCategorisedChanges().Any())
							{
								foreach (var uncat in changes.UnCategorisedChanges())
								{
									// write table here...
									html.WriteElement("div", () =>
									{
										foreach (var change in changes.UnCategorisedChanges())
										{
											object descriptor = change.Descriptor;

											if (descriptor != null)
											{
												IDocumentLink link = descriptor as IDocumentLink;

												if (link != null)
												{
													html.WriteElement("p", () =>
													{
														// project path index path...
														html.WriteLink(this.Map.LookupRelative(link.Identifier), link.LinkText);
													});
												}
											}
											else if (!String.IsNullOrEmpty(change.Description))
											{
												html.WriteElement("p", () =>
												{
													html.WriteString(change.Description);
												});
											}
										}
									});
								}
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

				if (this.Map != null)
				{
					output.File = this.Map.Lookup(changes.Identifier);
				}

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

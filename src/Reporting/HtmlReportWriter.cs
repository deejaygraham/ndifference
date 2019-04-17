using NDifference.Analysis;
using NDifference.SourceFormatting;
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
					string folder = Path.GetDirectoryName(output.Path);

					if (!Directory.Exists(folder))
					{
						Directory.CreateDirectory(folder);
					}
				}

				Encoding utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

				var settings = new XmlWriterSettings
				{
					Encoding = utf8,
					OmitXmlDeclaration = true,
					Indent = true
                };

				using (XmlWriter html = XmlWriter.Create(text, settings))
				{
					html.WriteStartDocument();

					html.WriteRaw("<!DOCTYPE html>\r\n");

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

							html.WriteNewLine();
							html.WriteComment(" " + changes.Identifier + " ");
                            html.WriteNewLine();

                            if (changes.MetaBlocks != null)
							{
								foreach (var meta in changes.MetaBlocks)
								{
									html.WriteRaw(meta);
                                    html.WriteNewLine();
                                }
                            }
						});

						html.WriteElement("body", () =>
						{
							foreach (var header in changes.HeaderBlocks)
							{
                                html.WriteNewLine();
                                html.WriteRaw(header);
                                html.WriteNewLine();
                            }

                            if (!String.IsNullOrEmpty(changes.HeadingBlock))
							{
								html.WriteElement("div", () =>
								{
									html.WriteAttributeString("class", "diff-header");

									if (!String.IsNullOrEmpty(changes.HeadingBlock))
									{
										html.WriteElement("div", () =>
										{
											html.WriteAttributeString("id", "exp");
											html.WriteRaw(changes.HeadingBlock);
										});
									}
								});
							}

							html.WriteElement("div", () =>
							{
								html.WriteAttributeString("class", "diff-container");

								html.WriteElement("div", () =>
								{
									html.WriteAttributeString("id", "summary");
                                    html.WriteNewLine();
                                    html.WriteComment(" Summary Table ");

                                    html.WriteNewLine();
                                    html.WriteElement("table", () =>
									{
										html.WriteAttributeString("class", "diff-table");
										html.WriteAttributeString("summary", "Summary of changes between versions");

                                        html.WriteNewLine();
                                        html.WriteElement("caption", () =>
										{
											html.WriteString("Summary of changes");
										});

                                        html.WriteNewLine();
                                        html.WriteElement("tbody", () =>
										{
                                            html.WriteNewLine();
                                            foreach (var key in changes.SummaryBlocks.Keys)
											{
												html.WriteTableRow(key, changes.SummaryBlocks[key]);
											}

											// write each category
											foreach (var cat in changes.Categories.OrderBy(x => x.Priority.Value))
											{
                                                var list = changes.ChangesInCategory(cat.Name);

                                                if (list.Any())
												{
													html.WriteTableRow(cat.Name, list.Count, "#" + cat.Identifier);
												}
											}
										});

                                        html.WriteNewLine();
                                    });

                                    html.WriteNewLine();
                                    html.WriteComment(" End of Summary Table ");
								});

								foreach (var cat in changes.Categories.OrderBy(x => x.Priority.Value))
								{
                                    var list = changes.ChangesInCategory(cat.Name);

                                    if (list.Any())
                                        RenderCategory(cat, list, html, output);
								}

								var uncatChanges = changes.UnCategorisedChanges();

								if (uncatChanges.Any())
								{
									html.WriteComment(" Writing Uncategorised changes ... ");

									var uncat = new Category { Priority = new CategoryPriority(999), Description = "Uncategorised changes", Name = "Uncategorised Changes" };

									RenderCategory(uncat, uncatChanges, html, output);
								}
							});

							foreach (var footer in changes.FooterBlocks)
							{
                                html.WriteNewLine();
                                html.WriteRaw(footer);
                                html.WriteNewLine();
                            }

                            // end of body
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
            html.WriteNewLine();
            html.WriteElement("div", () =>
            {
				html.WriteAttributeString("id", cat.Identifier.ToString());

				if (changes.Any())
				{
                    html.WriteNewLine();
                    html.WriteElement("h2", () =>
					{
						html.WriteString(cat.Name);
					});
                    html.WriteNewLine();

                    html.WriteComment("Category P" + cat.Priority.Value);

                    html.WriteNewLine();
                    html.WriteElement("table", () =>
					{
						html.WriteAttributeString("class", "diff-table");

						if (!String.IsNullOrEmpty(cat.Description))
						{
							html.WriteAttributeString("summary", cat.Description);

                            html.WriteNewLine();
                            html.WriteElement("caption", () =>
							{
								// new property on category - caption ?
								html.WriteString(cat.FullDescription);
							});
                            html.WriteNewLine();
                        }

                        if (cat.Headings != null && cat.Headings.Length > 0)
						{
                            html.WriteNewLine();
                            html.WriteElement("thead", () =>
							{
								html.WriteElement("tr", () =>
								{
									foreach (var head in cat.Headings)
									{
										html.WriteElement("th", () =>
										{
											html.WriteRaw(head);
										});
									}
								});
							});
                            html.WriteNewLine();
                        }

                        html.WriteElement("tbody", () =>
						{
                            html.WriteNewLine();
                            // order changes...
                            var ordered = new List<IdentifiedChange>(changes);
							ordered.Sort(new IdentifiedChangeComparer());

							foreach (var change in ordered)
							{
								RenderChange(change, html, output);
							}

                            html.WriteNewLine();
                        });
					});
				}
				else
				{
					html.WriteComment(" No " + cat.Name + " identified ");
				}

                html.WriteNewLine();
            });
		}

		private void RenderChange(IdentifiedChange change, XmlWriter html, IReportOutput output)
		{
			object descriptor = change.Descriptor;

			if (descriptor != null)
			{
				RenderDescriptor(change, descriptor, html, output);
			}
			else if (!String.IsNullOrEmpty(change.Description))
			{
				html.WriteTableRow(/*change.Inspector, */change.Description);
			}
		}

		private void RenderDescriptor(IdentifiedChange change, object descriptor, XmlWriter html, IReportOutput output)
		{
			IDocumentLink link = descriptor as IDocumentLink;

			if (link != null)
			{
				if (this.Map != null)
				{
					html.WriteTableRow(change.Inspector, link, output, this.Map);
				}
			}
			else
			{
				ICodeDescriptor code = descriptor as ICodeDescriptor;

				if (code != null)
				{
					html.WriteTableRow(change.Inspector, code, this._format);
				}
				else
				{
					INameValueDescriptor nvd = descriptor as INameValueDescriptor;

					if (nvd != null)
					{
						html.WriteTableRow(change.Inspector, nvd, this._format);
					}
					else
					{
						INamedDeltaDescriptor nd = descriptor as INamedDeltaDescriptor;

						if (nd != null)
						{
							html.WriteTableRow(change.Inspector, nd, this._format);
						}
						else
						{
							IValueDescriptor vd = descriptor as IValueDescriptor;

							if (vd != null)
							{
								html.WriteTableRow(change.Inspector, vd, this._format);
							}
							else
							{
								INamedDeltaDescriptor ndd = descriptor as INamedDeltaDescriptor;

								if (ndd != null)
								{
									html.WriteTableRow(change.Inspector, ndd, this._format);
								}
								else
								{
									IDeltaDescriptor delta = descriptor as IDeltaDescriptor;

									if (delta != null)
									{
										html.WriteTableRow(change.Inspector, delta, this._format);
									}
									//else
									//{
									//	INameValueDescriptor textDesc = descriptor as INameValueDescriptor;

									//	if (textDesc != null)
									//	{
									//		html.WriteTableRow(change.Inspector, textDesc, this._format);
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

using NDifference.Analysis;
using NDifference.SourceFormatting;
using System;
using System.Xml;

namespace NDifference.Reporting
{
	/// <summary>
	/// Html extensions
	/// </summary>
	public static class XmlWriterExtensions
	{
		const int MaxLineLength = 80;

		public static void WriteHtml4DocType(this XmlWriter writer)
		{
			writer.WriteRaw("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">\r\n");
		}

		public static void WriteContentType(this XmlWriter writer, string contentType)
		{
			writer.WriteNewLine();
			writer.WriteRaw("<meta http-equiv=\"Content-Type\" content=\"");
			writer.WriteRaw(contentType);
			writer.WriteRaw("\" >\r\n");
		}

		public static void WriteElement(this XmlWriter writer, string name, Action action)
		{
			writer.WriteStartElement(name);
			action();
			writer.WriteEndElement();
		}

		public static void WriteElement(this XmlWriter writer, string name, Action<XmlWriter> action)
		{
			writer.WriteStartElement(name);
			action(writer);
			writer.WriteEndElement();
		}

		public static void WriteHeading(this XmlWriter writer, int headingImportance, string heading)
		{
            writer.WriteHeading(headingImportance, string.Empty, heading);
		}

		public static void WriteHeading(this XmlWriter writer, int headingImportance, string id, string heading)
		{
            writer.WriteNewLine();
            writer.WriteElement(string.Format("h{0}", headingImportance), () =>
			{
                if (!string.IsNullOrEmpty(id))
				    writer.WriteAttributeString("id", id);

				writer.WriteString(heading);
			});
		}

		public static void WriteParagraph(this XmlWriter writer, string para)
		{
			writer.WriteElement("p", () =>
			{
				writer.WriteString(para);
			});
		}

		public static void WriteTableRow(this XmlWriter writer, params string[] cells)
		{
            writer.WriteNewLine();
            writer.WriteElement("tr", () =>
			{
                writer.WriteNewLine();

                for (int i = 0; i < cells.Length; ++i)
                {
                    writer.WriteElement("td", () =>
                    {
                        writer.WriteRaw(cells[i]);
                    });
                }
                writer.WriteNewLine();
            });
            writer.WriteNewLine();
        }

		public static void WriteTableRow(this XmlWriter writer, string shortCode, IDocumentLink change, IReportOutput output, FileMap map)
		{
			bool outputDeadLinks = false;

			// look up correct path...
			IFolder folder = new PhysicalFolder(System.IO.Path.GetDirectoryName(output.Path));

			string fullPath = map.PathFor(change.Identifier);

			bool linkIsGood = System.IO.File.Exists(fullPath);

			if (linkIsGood || outputDeadLinks)
			{
				writer.WriteElement("tr", () =>
				{
                    //writer.WriteElement("td", () =>
                    //{
                    //	writer.WriteRaw(shortCode);
                    //});
                    writer.WriteNewLine();
                    writer.WriteElement("td", () =>
					{
						if (linkIsGood)
						{
							string relativePath = map.PathRelativeTo(change.Identifier, folder);

							writer.RenderLink(map.PathRelativeTo(change.Identifier, folder), change.LinkText, change.LinkText);
						}
						else
						{
							writer.WriteRaw(change.LinkText);
						}
					});
				});
			}
		}

		public static void WriteTableRow(this XmlWriter writer, string shortCode, INameDescriptor change, IReportFormat format)
		{
            writer.WriteTableRow(change.Name);
		}

		public static void WriteTableRow(this XmlWriter writer, string shortCode, IValueDescriptor change, IReportFormat format)
		{
			string text = change.Value.ToString();

			ICoded code = change.Value as ICoded;

			if (code != null)
				text = format.Format(code);

            writer.WriteTableRow(text);
		}

		public static void WriteTableRow(this XmlWriter writer, string shortCode, INameValueDescriptor change, IReportFormat format)
		{
			string text = change.Value.ToString();

			ICoded code = change.Value as ICoded;

			if (code != null)
				text = format.Format(code);

            writer.WriteTableRow(change.Name, text);
		}

		public static void WriteTableRow(this XmlWriter writer, string shortCode, IDeltaDescriptor change, IReportFormat format)
		{
            writer.WriteTableRow(change.Was, change.IsNow);
		}

		public static void WriteTableRow(this XmlWriter writer, string shortCode, INamedDeltaDescriptor change, IReportFormat format)
		{
            writer.WriteTableRow(change.Name, change.Was, change.IsNow);
		}

		public static void WriteTableRow(this XmlWriter writer, string shortCode, ICodeDescriptor change, IReportFormat format)
		{
			string text = format.Format(change.Code);

            writer.WriteTableRow(text);
		}

		public static void WriteTableRowLink(this XmlWriter writer, string cell1, string cell2, string link)
		{
			writer.WriteElement("tr", () =>
			{
                writer.WriteNewLine();
                writer.WriteElement("td", () =>
				{
					writer.RenderLink(link, cell1, cell1);
				});
				writer.WriteElement("td", () =>
				{
					writer.RenderLink(link, cell2, cell2);
				});
			});
		}

		public static void WriteTableRow(this XmlWriter writer, string cell1, int cell2)
		{
			writer.WriteTableRow(cell1, cell2.ToString());
		}

		public static void WriteTableRow(this XmlWriter writer, string cell1, int cell2, string link)
		{
			writer.WriteTableRowLink(cell1, cell2.ToString(), link);
		}

		public static void RenderLink(this XmlWriter writer, string href, string title, string anchorText)
		{
            writer.WriteNewLine();
            writer.WriteElement("a", () =>
			{
				writer.WriteAttributeString("href", href.Replace('\\', '/'));
				writer.WriteAttributeString("title", title);
				writer.WriteString(anchorText);
			});
            writer.WriteNewLine();
        }

        public static void WriteNewLine(this XmlWriter writer)
        {
            writer.WriteWhitespace("\r\n");
        }
	}
}

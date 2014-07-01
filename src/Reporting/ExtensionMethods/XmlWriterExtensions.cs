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
			writer.WriteElement(string.Format("h{0}", headingImportance), () =>
			{
				writer.WriteString(heading);
			});
		}

		public static void WriteHeading(this XmlWriter writer, int headingImportance, string id, string heading)
		{
			writer.WriteElement(string.Format("h{0}", headingImportance), () =>
			{
				writer.WriteAttributeString("id", id);
				writer.WriteString(heading);
			});
		}

		public static void WriteLink(this XmlWriter writer, string link, string text)
		{
			writer.WriteElement("a", () =>
			{
				writer.WriteAttributeString("href", link.Replace('\\', '/'));
				writer.WriteString(text);
			});
		}

		public static void WriteParagraph(this XmlWriter writer, string para)
		{
			writer.WriteElement("p", () =>
			{
				writer.WriteString(para);
			});
		}

		public static void WriteTableRow(this XmlWriter writer, string cell)
		{
			writer.WriteElement("tr", () =>
			{
				writer.WriteElement("td", () =>
				{
					writer.WriteString(cell);
				});
			});
		}

		public static void WriteTableRow(this XmlWriter writer, string cell1, string cell2)
		{
			writer.WriteElement("tr", () =>
			{
				writer.WriteElement("td", () =>
				{
					writer.WriteString(cell1);
				});
				writer.WriteElement("td", () =>
				{
					writer.WriteString(cell2);
				});
			});
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
					writer.WriteElement("td", () =>
					{
						if (linkIsGood)
						{
							string relativePath = map.PathRelativeTo(change.Identifier, folder);

							writer.WriteLink(map.PathRelativeTo(change.Identifier, folder), change.LinkText);
						}
						else
						{
							writer.WriteRaw(change.LinkText);
						}
					});
				});
			}
		}

		public static void WriteTableRow(this XmlWriter writer, string shortCode, ITextDescriptor change, IReportFormat format)
		{
			string text = change.Message.ToString();

			ICoded code = change.Message as ICoded;

			if (code != null)
				text = format.Format(code);

			writer.WriteElement("tr", () =>
			{
				//writer.WriteElement("td", () =>
				//{
				//	writer.WriteRaw(shortCode);
				//});
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(change.Name);
				});
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(text);
				});
			});
		}

		public static void WriteTableRow(this XmlWriter writer, string shortCode, IDeltaDescriptor change, IReportFormat format)
		{
			string wasText = change.Was.ToString();
			string isText = change.IsNow.ToString();

			ICoded was = change.Was as ICoded;
			ICoded isNow = change.IsNow as ICoded;

			if (was != null)
			{
				wasText = format.Format(was);
			}

			if (isNow != null)
			{
				isText = format.Format(isNow);
			}

			writer.WriteElement("tr", () =>
			{
				//writer.WriteElement("td", () =>
				//{
				//	writer.WriteRaw(shortCode);
				//});
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(change.Name);
				});
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(wasText);
				});
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(isText);
				});
			});
		}

		public static void WriteTableRow(this XmlWriter writer, string shortCode, ICodeDescriptor change, IReportFormat format)
		{
			string text = format.Format(change.Code);

			writer.WriteElement("tr", () =>
			{
				//writer.WriteElement("td", () =>
				//{
				//	writer.WriteRaw(shortCode);
				//});
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(text);
				});
			});
		}

		public static void WriteTableRowRaw(this XmlWriter writer, string cell1, string cell2)
		{
			writer.WriteElement("tr", () =>
			{
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(cell1);
				});
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(cell2);
				});
			});
		}

		public static void WriteTableRowRaw(this XmlWriter writer, string cell1, string cell2, string cell3)
		{
			writer.WriteElement("tr", () =>
			{
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(cell1);
				});
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(cell2);
				});
				writer.WriteElement("td", () =>
				{
					writer.WriteRaw(cell3);
				});
			});
		}

		public static void WriteTableRowLink(this XmlWriter writer, string cell1, string cell2, string link)
		{
			writer.WriteElement("tr", () =>
			{
				writer.WriteElement("td", () =>
				{
					writer.WriteElement("a", () =>
					{
						writer.WriteAttributeString("href", link);
						writer.WriteString(cell1);
					});
				});
				writer.WriteElement("td", () =>
				{
					writer.WriteElement("a", () =>
					{
						writer.WriteAttributeString("href", link);
						writer.WriteString(cell2);
					});
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
	}
}

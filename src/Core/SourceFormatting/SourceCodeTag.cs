﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NDifference.SourceFormatting
{
	public class SourceCodeTag : IXmlSerializable
	{
		public SourceCodeTag()
		{
		}

		public SourceCodeTag(string tagName, string tagValue)
		{
			this.Name = tagName;
			this.Value = tagValue;
		}

		public string Name
		{
			get;
			set;
		}

		public string Value
		{
			get;
			set;
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			/* nothing - output only */
		}

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(this.Name);
			writer.WriteRaw(this.Value);
			writer.WriteEndElement();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public override string ToString()
		{
			string output = string.Empty;

			TextWriter writer = null;

			try
			{
				writer = new StringWriter();

				using (XmlWriter xmlWriter = new XmlTextWriter(writer))
				{
					this.WriteXml(xmlWriter);
					output = writer.ToString();
				}
			}
			finally
			{
				if (writer != null)
				{
					writer.Dispose();
					writer = null;
				}
			}

			return output;
		}

		//public void SaveTo(TextWriter writer)
		//{
		//	ObjectFlattener<SourceCodeTag>.Flatten(this, writer);
		//}

		//public string ToXml()
		//{
		//	using (var writer = new StringWriter())
		//	{
		//		this.SaveTo(writer);
		//		return writer.ToString();
		//	}
		//}

	}
}

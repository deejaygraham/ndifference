using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NDifference.SourceFormatting
{
	/// <summary>
	/// Load and save objects to xml text.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal static class ObjectFlattener<T> where T : new()
	{
		public static string Flatten(T instance)
		{
			string output = string.Empty;

			using (var writer = new StringWriter())
			{
				Flatten(instance, writer);
				output = writer.ToString();
			}

			return output;
		}

		public static void Flatten(T instance, TextWriter writer)
		{
			var settings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				OmitXmlDeclaration = true,
				Indent = true,
				IndentChars = "\t"
			};

			using (XmlWriter html = XmlTextWriter.Create(writer, settings))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				serializer.Serialize(html, instance);
			}
		}
	}
}

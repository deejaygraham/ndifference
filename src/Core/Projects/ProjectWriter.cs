using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NDifference.Projects
{
	/// <summary>
	/// Writes Project object to disk.
	/// </summary>
	public static class ProjectWriter
	{
		public static void Save(Project project, string toFile)
		{
			project.FileName = toFile;

			const bool AppendToFile = false;

			using (var writer = new StreamWriter(toFile, AppendToFile, System.Text.Encoding.UTF8))
			{
				ProjectWriter.SaveTo(project, writer);
			}
		}

		public static void SaveTo(Project project, TextWriter writer)
		{
			Debug.Assert(project != null, "Project cannot be null");
			Debug.Assert(writer != null, "TextWriter cannot be null");

			Debug.Assert(!string.IsNullOrEmpty(project.Version), "Project version cannot be blank");

			var pff = project.ToPersistableFormat();

			using (XmlWriter xml = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
			{
				XmlSerializer serial = new XmlSerializer(typeof(PersistableProject));
				serial.Serialize(writer, pff);
			}
		}
	}
}

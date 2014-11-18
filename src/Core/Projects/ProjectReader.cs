using NDifference.Exceptions;
using System;
using System.IO;
using System.Xml.Serialization;

namespace NDifference.Projects
{
	/// <summary>
	/// Reads project files from disk and creates a Project object.
	/// </summary>
	public static class ProjectReader
	{
		public static Project LoadFromFile(string fileName)
		{
			using (var reader = new StreamReader(fileName))
			{
				Project project = ProjectReader.LoadFrom(reader, Path.GetDirectoryName(fileName));
				project.FileName = fileName;

				return project;
			}
		}

		public static Project LoadFrom(TextReader reader)
		{
			return ProjectReader.LoadFrom(reader, string.Empty);
		}

		public static Project LoadFrom(TextReader reader, string baseFolder)
		{
			try
			{
				XmlSerializer serial = new XmlSerializer(typeof(PersistableProject));
				PersistableProject pff = (PersistableProject)serial.Deserialize(reader);

				return Project.FromPersistableFormat(pff, baseFolder);
			}
			catch (InvalidOperationException)
			{
				throw new InvalidProjectFileFormatException();
			}
		}
	}
}

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NDifference.Reporting
{
	public class FileOutput : IReportOutput
	{
		public FileOutput(IFile file)
		{
			this.Path = file.FullPath;
			this.Folder = file.Folder;
		}

		public FileOutput(string fullPath)
		{
			Debug.Assert(!string.IsNullOrEmpty(fullPath), "Filename cannot be blank");

			if (System.IO.Path.IsPathRooted(fullPath))
			{
				this.Folder = System.IO.Path.GetDirectoryName(fullPath);
				this.Path = fullPath;
			}
			else
			{
				this.Folder = Directory.GetCurrentDirectory();
				this.Path = System.IO.Path.Combine(this.Folder, fullPath);
			}
		}

		public string Folder { get; private set; }

		public string Path { get; private set; }

		public void Execute(string reportContent)
		{
			Debug.Assert(!String.IsNullOrEmpty(this.Path), "File not set");

			if (!Directory.Exists(this.Folder))
			{
				Directory.CreateDirectory(this.Folder);
			}

			string fullPath = System.IO.Path.Combine(this.Folder, this.Path);

			if (System.IO.File.Exists(fullPath))
			{
				System.IO.File.Delete(fullPath);
			}

			const bool AppendToFile = false;
			const bool includeBOM = false;

			using (var writer = new StreamWriter(fullPath, AppendToFile, new UTF8Encoding(includeBOM)))
			{
				writer.Write(reportContent);
			}
		}
	}
}

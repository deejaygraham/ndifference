using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NDifference.Reporting
{
	public class FileOutput : IReportOutput
	{
		public FileOutput(string fileName)
		{
			Debug.Assert(!string.IsNullOrEmpty(fileName), "Filename cannot be blank");

			if (Path.IsPathRooted(fileName))
			{
				this.Folder = Path.GetDirectoryName(fileName);
			}
			else
			{
				this.Folder = Directory.GetCurrentDirectory();
			}

			this.File = Path.GetFileName(fileName);
		}

		public string Folder { get; set; }

		public string File { get; set; }

		public void Execute(string reportContent)
		{
			Debug.Assert(!String.IsNullOrEmpty(this.Folder), "Folder not set");
			Debug.Assert(!String.IsNullOrEmpty(this.File), "File not set");

			if (!Directory.Exists(this.Folder))
			{
				Directory.CreateDirectory(this.Folder);
			}

			string fullPath = Path.Combine(this.Folder, this.File);

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

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NDifference
{
	public class FileFinder : IFileFinder
	{
		public FileFinder()
			: this(string.Empty)
		{
		}

		public FileFinder(string folder)
			: this(folder, FileFilterConstants.AllFilesFilter)
		{
		}

		public FileFinder(string folder, string filter)
		{
			this.Folder = folder;
			this.Filter = filter;
		}

		public string Folder { get; set; }

		public string Filter { get; set; }
		
		public IEnumerable<IFile> Find()
		{
			Debug.Assert(!string.IsNullOrEmpty(this.Folder), "Folder cannot be blank");
			Debug.Assert(!string.IsNullOrEmpty(this.Filter), "Filter is not set");
			Debug.Assert(Directory.Exists(this.Folder), "Folder does not exist");

			foreach(string file in Directory.GetFiles(this.Folder, this.Filter))
				yield return new PhysicalFile(file);

			yield break;
		}

		public IEnumerable<FileInfo> FileInfoFind()
		{
			Debug.Assert(!string.IsNullOrEmpty(this.Folder), "Folder cannot be blank");
			Debug.Assert(!string.IsNullOrEmpty(this.Filter), "Filter is not set");
			Debug.Assert(Directory.Exists(this.Folder), "Folder does not exist");

			foreach (string file in Directory.GetFiles(this.Folder, this.Filter))
			{
				yield return new FileInfo(file);
			}

			yield break;
		}

	}
}

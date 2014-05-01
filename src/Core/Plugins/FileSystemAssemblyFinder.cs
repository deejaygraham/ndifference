using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Plugins
{
	internal class FileSystemAssemblyFinder : IFileFinder
	{
		public FileSystemAssemblyFinder(string folder)
		{
			this.Folder = folder;
			this.Filter = FileFilterConstants.AssemblyFilter;
		}

		public string Folder { get; set; }

		public string Filter { get; set; }
		
		public IEnumerable<string> Find()
		{
			Debug.Assert(!string.IsNullOrEmpty(this.Folder), "Folder cannot be blank");
			Debug.Assert(!string.IsNullOrEmpty(this.Filter), "Filter is not set");

			foreach(string file in System.IO.Directory.GetFiles(this.Folder, this.Filter))
				yield return file;

			yield break;
		}
	}
}

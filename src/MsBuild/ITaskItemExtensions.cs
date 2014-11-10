using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Tasks
{
	public static class ITaskItemExtensions
	{
		public static string GetFullPath(this ITaskItem item)
		{
			return item.GetMetadata("FullPath");
		}

		public static void ValidateFile(this ITaskItem file)
		{
			if (file == null)
				return;

			string path = file.GetFullPath();

			if (!File.Exists(path))
			{
				throw new FileNotFoundException(string.Format("File \'{0}\' does not exist.", path));
			}
		}

		public static void ValidateFiles(this IEnumerable<ITaskItem> files)
		{
			foreach (ITaskItem file in files)
				file.ValidateFile();
		}

		public static void ValidateFolder(this ITaskItem folder)
		{
			string path = folder.GetFullPath();

			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException(string.Format("Folder \'{0}\' does not exist.", path));
			}
		}
	}
}

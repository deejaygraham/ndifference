using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Files
{
	public class AssemblyDiskInfoBuilder
	{
		public static IEnumerable<AssemblyDiskInfo> BuildFromFolder(string folder)
		{
			var finder = new FileFinder(folder, FileFilterConstants.AssemblyFilter);

			foreach (var file in finder.Find())
				yield return BuildFromFile(file);

			yield break;
		}

		public static AssemblyDiskInfo BuildFromFile(string path)
		{
			return new AssemblyDiskInfo(path);
		}

		public static IEnumerable<AssemblyDiskInfo> BuildFrom(DirectoryInfo info)
		{
			var finder = new FileFinder(info.FullName, FileFilterConstants.AssemblyFilter);

			foreach (var fileInfo in finder.FileInfoFind())
				yield return BuildFrom(fileInfo);

			yield break;
		}

		public static AssemblyDiskInfo BuildFrom(FileInfo info)
		{
			return new AssemblyDiskInfo(
						info.FullName,
						info.CreationTimeUtc,
						info.Length,
						info.CalculateChecksum());
		}
	}
}

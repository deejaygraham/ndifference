using System.Collections.Generic;
using System.IO;

namespace NDifference.Files
{
	public class AssemblyDiskInfoBuilder
	{
		public static IEnumerable<IAssemblyDiskInfo> BuildFromFolder(string folder)
		{
			var finder = new FileFinder(folder, FileFilterConstants.AssemblyFilter);

			foreach (var file in finder.Find())
				yield return BuildFromFile(file);

			yield break;
		}

		public static IAssemblyDiskInfo BuildFromFile(string path)
		{
			return new AssemblyDiskInfo(path);
		}

		public static IEnumerable<IAssemblyDiskInfo> BuildFrom(DirectoryInfo info)
		{
			var finder = new FileFinder(info.FullName, FileFilterConstants.AssemblyFilter);

			foreach (var fileInfo in finder.FileInfoFind())
				yield return BuildFrom(fileInfo);

			yield break;
		}

		public static IAssemblyDiskInfo BuildFrom(FileInfo info)
		{
			return new AssemblyDiskInfo(
						info.FullName,
						info.CreationTimeUtc,
						info.Length,
						info.CalculateChecksum());
		}
	}
}

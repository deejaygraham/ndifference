using System.Collections.Generic;
using System.IO;

namespace NDifference
{
	public class AssemblyDiskInfoBuilder
    {
		public IEnumerable<IAssemblyDiskInfo> BuildFromFolder(string folder)
        {
            if (!Directory.Exists(folder)) throw new DirectoryNotFoundException(string.Format("Folder \'{0}\' does not exist.", folder));

			var finder = new FileFinder(folder, FileFilterConstants.AssemblyFilter);

            foreach (var file in finder.Find())
                yield return BuildFrom(new FileInfo(file.FullPath));

            yield break;
        }

        public IAssemblyDiskInfo BuildFromFile(string path)
        {
            string folder = Path.GetDirectoryName(path);

            if (!Directory.Exists(folder)) throw new DirectoryNotFoundException(string.Format("Folder \'{0}\' does not exist.", folder));

			return BuildFrom(new FileInfo(path));
		}

		//public static IEnumerable<IAssemblyDiskInfo> BuildFrom(DirectoryInfo info)
		//{
		//	var finder = new FileFinder(info.FullName, FileFilterConstants.AssemblyFilter);

		//	foreach (var fileInfo in finder.FileInfoFind())
		//		yield return BuildFrom(fileInfo);

		//	yield break;
		//}

		public IAssemblyDiskInfo BuildFrom(FileInfo info)
		{
			return new AssemblyDiskInfo(
						info.FullName,
						info.CreationTimeUtc,
						info.Length);
		}
	}
}

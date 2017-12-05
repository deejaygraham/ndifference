using System;
using System.Diagnostics;
using System.IO;

namespace NDifference
{
	public static class PathExtensions
	{
        /// <summary>
        /// Creates a relative path from a file to a folder.
        /// </summary>
        public static string MakeRelativeToFolder(this string file, string parentFolder)
        {
            // Must end in a slash to indicate folder
            if (!parentFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                parentFolder += Path.DirectorySeparatorChar;
            
            Uri folderUri = new Uri(parentFolder);

            Uri fileUri = new Uri(file);

            return Uri.UnescapeDataString(
                folderUri.MakeRelativeUri(fileUri)
                    .ToString()
                    .Replace('/', Path.DirectorySeparatorChar)
                );
        }

        public static string MakeFoldersRelative(this string folder, string referenceFolder)
        {
            // Must end in a slash to indicate folder
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                folder += Path.DirectorySeparatorChar;

            if (!referenceFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                referenceFolder += Path.DirectorySeparatorChar;

            Uri folderUri = new Uri(folder);
            Uri referenceFolderUri = new Uri(referenceFolder);
            
            return Uri.UnescapeDataString(
                referenceFolderUri.MakeRelativeUri(folderUri)
                    .ToString()
                    .Replace('/', Path.DirectorySeparatorChar)
                );
        }

		public static string MakeAbsolutePath(this string basePath, string relativePath)
		{
			Debug.Assert(!string.IsNullOrEmpty(basePath), "Base path cannot be null");
			Debug.Assert(!string.IsNullOrEmpty(relativePath), "Relative path cannot be null");

			return Path.GetFullPath(Path.Combine(basePath, relativePath));
		}
	}
}

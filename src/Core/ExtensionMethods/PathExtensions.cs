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

  //      /// <summary>
  //      /// Creates a relative path from one file to another.
  //      /// </summary>
  //      /// <param name="absolutePath">Contains the directory that defines the start of the relative path.</param>
  //      /// <param name="file">Contains the path that defines the endpoint of the relative path.</param>
  //      /// <returns>The relative path from the start directory to the end path.</returns>
  //      /// <exception cref="ArgumentNullException"></exception>
  //      /// <exception cref="UriFormatException"></exception>
  //      /// <exception cref="InvalidOperationException"></exception>
  //      public static string MakeRelativeTo(this string file, string absolutePath)
		//{
		//	Debug.Assert(!string.IsNullOrEmpty(absolutePath), "Base path cannot be null");
		//	Debug.Assert(!string.IsNullOrEmpty(file), "Fully qualified path cannot be null");

  //          if (fullPath.StartsWith(workingDirectory))
  //          {
  //              return fullPath.Substring(workingDirectory.Length + 1);
  //          }

  //          Uri baseUri = new Uri(basePath);
		//	Uri fullyQualifiedUri = new Uri(fullyQualifiedPath);

		//	if (baseUri.Scheme != fullyQualifiedUri.Scheme)
		//	{
		//		return fullyQualifiedPath;
		//	}

		//	Uri relativeUri = baseUri.MakeRelativeUri(fullyQualifiedUri);
		//	string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

		//	const string FileScheme = "FILE";

		//	if (fullyQualifiedUri.Scheme.ToUpperInvariant() == FileScheme)
		//	{
		//		relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		//	}

		//	return relativePath;
		//}

		public static string MakeAbsolutePath(this string basePath, string relativePath)
		{
			Debug.Assert(!string.IsNullOrEmpty(basePath), "Base path cannot be null");
			Debug.Assert(!string.IsNullOrEmpty(relativePath), "Relative path cannot be null");

			return Path.GetFullPath(Path.Combine(basePath, relativePath));
		}
	}
}

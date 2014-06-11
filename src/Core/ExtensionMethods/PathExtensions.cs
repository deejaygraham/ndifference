using System;
using System.Diagnostics;
using System.IO;

namespace NDifference
{
	public static class PathExtensions
	{
		/// <summary>
		/// Creates a relative path from one file or folder to another.
		/// </summary>
		/// <param name="basePath">Contains the directory that defines the start of the relative path.</param>
		/// <param name="fullyQualifiedPath">Contains the path that defines the endpoint of the relative path.</param>
		/// <returns>The relative path from the start directory to the end path.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="UriFormatException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public static string MakeRelativePath(this string basePath, string fullyQualifiedPath)
		{
			Debug.Assert(!string.IsNullOrEmpty(basePath), "Base path cannot be null");
			Debug.Assert(!string.IsNullOrEmpty(fullyQualifiedPath), "Fully qualified path cannot be null");

			Uri baseUri = new Uri(basePath);
			Uri fullyQualifiedUri = new Uri(fullyQualifiedPath);

			if (baseUri.Scheme != fullyQualifiedUri.Scheme)
			{
				return fullyQualifiedPath;
			}

			Uri relativeUri = baseUri.MakeRelativeUri(fullyQualifiedUri);
			string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

			const string FileScheme = "FILE";

			if (fullyQualifiedUri.Scheme.ToUpperInvariant() == FileScheme)
			{
				relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			}

			return relativePath;
		}

		public static string MakeAbsolutePath(this string basePath, string relativePath)
		{
			Debug.Assert(!string.IsNullOrEmpty(basePath), "Base path cannot be null");
			Debug.Assert(!string.IsNullOrEmpty(relativePath), "Relative path cannot be null");

			return Path.GetFullPath(Path.Combine(basePath, relativePath));
		}
	}
}

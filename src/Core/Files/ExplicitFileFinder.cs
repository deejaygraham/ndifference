using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NDifference
{
	/// <summary>
	/// Finds only an explicitly supplied set of files.
	/// </summary>
	public sealed class ExplicitFileFinder : IFileFinder
	{
		private readonly string[] paths;

		public ExplicitFileFinder(params string[] paths)
		{
			if (paths == null)
			{
				throw new ArgumentNullException(nameof(paths));
			}

			this.paths = paths
				.Where(path => !string.IsNullOrWhiteSpace(path))
				.Select(Path.GetFullPath)
				.ToArray();
			this.Filter = FileFilterConstants.AllFilesFilter;
		}

		public string Filter { get; set; }

		public IEnumerable<IFile> Find()
		{
			foreach (string path in this.paths)
			{
				if (File.Exists(path) && this.MatchesFilter(path))
				{
					yield return new PhysicalFile(path);
				}
			}
		}

		private bool MatchesFilter(string path)
		{
			if (string.IsNullOrWhiteSpace(this.Filter)
				|| string.Equals(this.Filter, "*", StringComparison.Ordinal)
				|| string.Equals(this.Filter, FileFilterConstants.AllFilesFilter, StringComparison.Ordinal))
			{
				return true;
			}

			if (this.Filter.StartsWith("*.", StringComparison.Ordinal))
			{
				return string.Equals(
					Path.GetExtension(path),
					this.Filter.Substring(1),
					StringComparison.OrdinalIgnoreCase);
			}

			return string.Equals(
				Path.GetFileName(path),
				this.Filter,
				StringComparison.OrdinalIgnoreCase);
		}
	}
}

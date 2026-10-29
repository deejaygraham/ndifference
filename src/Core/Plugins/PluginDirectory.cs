using System;
using System.IO;

namespace NDifference.Plugins
{
	/// <summary>
	/// Represents a directory which may contain discoverable third-party plugins.
	/// </summary>
	public sealed class PluginDirectory
	{
		public PluginDirectory(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException("Plugin directory path cannot be blank.", nameof(path));
			}

			this.Path = System.IO.Path.GetFullPath(path);
		}

		public string Path { get; private set; }

		public static PluginDirectory ForCurrentUser()
		{
			string localApplicationData = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData);

			return new PluginDirectory(
				System.IO.Path.Combine(localApplicationData, "NDifference", "Plugins"));
		}

		public IFileFinder CreateFinder()
		{
			Directory.CreateDirectory(this.Path);

			return new FileFinder(this.Path, FileFilterConstants.AssemblyFilter);
		}
	}
}

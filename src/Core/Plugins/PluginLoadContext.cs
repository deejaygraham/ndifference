using NDifference.Inspectors;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace NDifference.Plugins
{
	internal sealed class PluginLoadContext : AssemblyLoadContext
	{
		private readonly string pluginAssemblyPath;
		private readonly string pluginDirectory;

		public PluginLoadContext(string pluginAssemblyPath)
		{
			if (string.IsNullOrWhiteSpace(pluginAssemblyPath))
			{
				throw new ArgumentException("Plugin assembly path cannot be blank.", nameof(pluginAssemblyPath));
			}

			this.pluginAssemblyPath = Path.GetFullPath(pluginAssemblyPath);
			this.pluginDirectory = Path.GetDirectoryName(this.pluginAssemblyPath);
		}

		public Assembly LoadPluginAssembly()
		{
			return this.LoadFromAssemblyPath(this.pluginAssemblyPath);
		}

		protected override Assembly Load(AssemblyName assemblyName)
		{
			if (IsSharedAssembly(assemblyName))
			{
				return null;
			}

			string candidatePath = Path.Combine(this.pluginDirectory, assemblyName.Name + ".dll");

			if (!File.Exists(candidatePath))
			{
				return null;
			}

			return this.LoadFromAssemblyPath(candidatePath);
		}

		private static bool IsSharedAssembly(AssemblyName assemblyName)
		{
			string name = assemblyName.Name;

			return string.Equals(
					name,
					typeof(IInspector).GetTypeInfo().Assembly.GetName().Name,
					StringComparison.OrdinalIgnoreCase)
				|| string.Equals(name, "mscorlib", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(name, "netstandard", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(name, "System", StringComparison.OrdinalIgnoreCase)
				|| name.StartsWith("System.", StringComparison.OrdinalIgnoreCase)
				|| name.StartsWith("Microsoft.", StringComparison.OrdinalIgnoreCase);
		}
	}
}

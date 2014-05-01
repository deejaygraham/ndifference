using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Plugins
{
	/// <summary>
	/// Attempts to find assemblies on disk that contain implmentations of T.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal class PluginDiscoverer<T> where T : class
	{
		public PluginDiscoverer(IFileFinder finder)
		{
			Debug.Assert(finder != null, "Finder cannot be null");

			this.AssemblyFinder = finder;
			this.Instantiator = new ObjectInstantiator<T>();
			this.IgnoreAssemblies = new HashSet<string>(new CaseInsensitiveFileNameComparer());
		}

		public IFileFinder AssemblyFinder { get; set; }

		public ObjectInstantiator<T> Instantiator { get; set; }

		private HashSet<string> IgnoreAssemblies { get; set; }

		public void Ignore(string assemblyName)
		{
			Debug.Assert(!string.IsNullOrEmpty(assemblyName), "Assembly name is blank");
			
			this.IgnoreAssemblies.Add(assemblyName);
		}

		public List<T> Find()
		{
			Debug.Assert(this.AssemblyFinder != null, "Finder is not set");
			Debug.Assert(typeof(T).IsInterface, "Plugins are only supported for interfaces");

			List<T> found = new List<T>();

			IAssemblyReflectorFactory factory = new MsReflectorFactory();

			foreach (var file in this.AssemblyFinder.Find())
			{
				// filter out assembly names...
				if (this.FilterFile(file))
					continue;

				var reflector = factory.LoadAssembly(file);

				Debug.Assert(reflector != null, "Reflector not loaded correctly");

				var allPublicTypes = reflector.GetTypes(AssemblyReflectionOption.Public);

				if (allPublicTypes.Any())
				{
					Debug.Assert(this.Instantiator != null, "Instantiator is not set");

					found.AddRange(this.Instantiator.CreateTypesImplementingInterface(reflector));
				}
			}

			return found;
		}

		private bool FilterFile(string path)
		{
			string candidateFile = System.IO.Path.GetFileName(path);

			return this.IgnoreAssemblies.Contains(candidateFile);
		}
	}

	internal class CaseInsensitiveFileNameComparer : IEqualityComparer<string>
	{
		public bool Equals(string x, string y)
		{
			return x.Equals(y, System.StringComparison.OrdinalIgnoreCase);
		}

		public int GetHashCode(string obj)
		{
			return obj.ToUpperInvariant().GetHashCode();
		}
	}
}

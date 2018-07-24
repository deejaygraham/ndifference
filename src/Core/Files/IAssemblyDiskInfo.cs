using System;
using System.Collections.Generic;
using System.Linq;

namespace NDifference
{
	/// <summary>
	/// Information about an assembly as it is on disk
	/// </summary>
	public interface IAssemblyDiskInfo : IUniquelyIdentifiable, IEquatable<IAssemblyDiskInfo>, IHashable
	{
		/// <summary>
		/// File name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Full path to assembly
		/// </summary>
		string Path { get; }

		/// <summary>
		/// Date last modified
		/// </summary>
		DateTime Date { get; }

		/// <summary>
		/// Size in bytes
		/// </summary>
		long Size { get; }

		/// <summary>
		/// MD5 checksum
		/// </summary>
		string Checksum { get; }
	}

	public static class IAssemblyDiskInfoExtensions
	{
		/// <summary>
		/// Find a match for a specific assembly in a list.
		/// </summary>
		/// <param name="assemblies"></param>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static IAssemblyDiskInfo FindMatchFor(this IEnumerable<IAssemblyDiskInfo> assemblies, IAssemblyDiskInfo instance)
		{
			return assemblies.FindMatchFor(instance, new AssemblyNameComparer());
		}

		public static IAssemblyDiskInfo FindMatchFor(this IEnumerable<IAssemblyDiskInfo> assemblies, IAssemblyDiskInfo instance, IEqualityComparer<IAssemblyDiskInfo> comparison)
		{
			return assemblies.FirstOrDefault(x => comparison.Equals(x, instance));
		}

		public static IAssemblyDiskInfo FindMatchFor(this IEnumerable<IAssemblyDiskInfo> assemblies, string instance)
		{
			return assemblies.FirstOrDefault(x => x.Name.Equals(instance, StringComparison.CurrentCultureIgnoreCase));
		}

        public static bool AllAssembliesInSameFolder(this IEnumerable<IAssemblyDiskInfo> assemblies)
        {
            if (!assemblies.Any())
                return false;

            string folder = System.IO.Path.GetDirectoryName(assemblies.First().Path);
            return assemblies.All(x => System.IO.Path.GetDirectoryName(x.Path) == folder);
        }
	}
}

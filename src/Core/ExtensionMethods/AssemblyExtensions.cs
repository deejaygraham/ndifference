using System;
using System.Collections.Generic;
using System.Linq;

namespace NDifference
{
	public static class OpaqueAssemblyExtensions
	{
		/// <summary>
		/// Find a match for a specific assembly in a list.
		/// </summary>
		/// <param name="packages"></param>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static OpaqueAssembly FindMatchFor(this IEnumerable<OpaqueAssembly> packages, OpaqueAssembly instance)
		{
			return packages.FindMatchFor(instance, new OpaqueAssemblyNameComparer());
		}

		public static OpaqueAssembly FindMatchFor(this IEnumerable<OpaqueAssembly> packages, OpaqueAssembly instance, IEqualityComparer<OpaqueAssembly> comparison)
		{
			return packages.FirstOrDefault(x => comparison.Equals(x, instance));
		}
	}
}

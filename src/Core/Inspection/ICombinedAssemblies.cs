using NDifference.Analysis;
using System.Collections.Generic;

namespace NDifference.Inspection
{
	/// <summary>
	/// Collection of all assemblies from earlier and later revisions combined.
	/// </summary>
	public interface ICombinedAssemblies : IChurnable
	{
		/// <summary>
		/// List of all paired assemblies.
		/// </summary>
		IEnumerable<Pair<IAssemblyDiskInfo>> Assemblies { get; }

		/// <summary>
		/// All assemblies in common.
		/// </summary>
		IEnumerable<Pair<IAssemblyDiskInfo>> InCommon { get; }

		/// <summary>
		/// Assemblies only present in early revision.
		/// </summary>
		IEnumerable<Pair<IAssemblyDiskInfo>> InEarlierOnly { get; }

		/// <summary>
		/// Assemblies only present in later revision.
		/// </summary>
		IEnumerable<Pair<IAssemblyDiskInfo>> InLaterOnly { get; }

		/// <summary>
		/// Common assemblies that fail checksum comparison.
		/// </summary>
		IEnumerable<Pair<IAssemblyDiskInfo>> ChangedInCommon { get; }
	}
}

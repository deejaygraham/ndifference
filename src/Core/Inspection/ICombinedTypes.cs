using NDifference.Analysis;
using NDifference.TypeSystem;
using System.Collections.Generic;

namespace NDifference.Inspection
{
	/// <summary>
	/// Combined collection of all paired types.
	/// </summary>
	public interface ICombinedTypes : IChurnable
	{
		/// <summary>
		/// Complete list of types.
		/// </summary>
		IEnumerable<Pair<ITypeInfo>> Types { get; }

		/// <summary>
		/// Types in common between earlier and later revisions.
		/// </summary>
		IEnumerable<Pair<ITypeInfo>> InCommon { get; }

		/// <summary>
		/// Types present only in earlier revision.
		/// </summary>
		IEnumerable<Pair<ITypeInfo>> InEarlierOnly { get; }

		/// <summary>
		/// Types present only in later revision.
		/// </summary>
		IEnumerable<Pair<ITypeInfo>> InLaterOnly { get; }

		/// <summary>
		/// Common types that failed checksum validation.
		/// </summary>
		IEnumerable<Pair<ITypeInfo>> ChangedInCommon { get; }
	}
}

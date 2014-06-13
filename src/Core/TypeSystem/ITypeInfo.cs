using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// A type discovered during reflection of an assembly.
	/// </summary>
	public interface ITypeInfo : IMaybeObsolete, IUniquelyIdentifiable, IHashable, IEquatable<ITypeInfo>, ISourceCodeProvider
	{
		/// <summary>F
		/// The "kind" of object this is - enum, interface, class etc.
		/// </summary>
		TypeTaxonomy Taxonomy { get; }

		string Assembly { get; }

		string Namespace { get; }

		string Name { get; }

		string FullName { get; }

		AccessModifier Access { get; }
	}

	public static class ITypeInfoExtensions
	{
		public static ITypeInfo FindMatchFor(this IEnumerable<ITypeInfo> types, string instance)
		{
			return types.FirstOrDefault(x => x.FullName.Equals(instance, StringComparison.CurrentCultureIgnoreCase));
		}
	}
}

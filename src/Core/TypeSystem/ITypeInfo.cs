
using System;
namespace NDifference.TypeSystem
{
	/// <summary>
	/// A type discovered during reflection of an assembly.
	/// </summary>
	public interface ITypeInfo : IMaybeObsolete, IUniquelyIdentifiable, IHashable, IEquatable<ITypeInfo>
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

		///// <summary>
		///// The name of the type.
		///// </summary>
		//FullyQualifiedName FullName { get; }

		string CalculateHash();
	}
}

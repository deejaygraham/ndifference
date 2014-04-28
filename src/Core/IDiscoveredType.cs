
namespace NDifference
{
	/// <summary>
	/// A type discovered in during reflection of an assembly.
	/// </summary>
	public interface IDiscoveredType : IMaybeObsolete, IUniquelyIdentifiable
	{
		/// <summary>
		/// The name of the type.
		/// </summary>
		FullyQualifiedName FullName { get; }
	}
}

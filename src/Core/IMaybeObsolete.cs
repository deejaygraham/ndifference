namespace NDifference
{
	/// <summary>
	/// Has this been made obsolete?
	/// </summary>
	public interface IMaybeObsolete
	{
		/// <summary>
		/// If obsolete, property will be non-null.
		/// </summary>
		Obsolete ObsoleteMarker { get; set; }
	}
}

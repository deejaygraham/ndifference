namespace NDifference.Inspectors
{
	/// <summary>
	/// Common interface used by all inspectors at every level,
	/// e.g. looking at collection of assemblies, individual assemblies,
	/// or individual types.
	/// </summary>
	public interface IInspector
	{
        /// <summary>
		/// Is this inspector enabled for this run?
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		/// Unique three-letter code used to enable/disable in the project file.
		/// </summary>
		string ShortCode { get; }

		/// <summary>
		/// Human readable name shown in the UI for this inspector.
		/// </summary>
		string DisplayName { get; }

		/// <summary>
		/// Human readable description of the purpose of this inspector.
		/// </summary>
		string Description { get; }
	}
}

namespace NDifference
{
	/// <summary>
	/// Creates assembly info suitable for examination by rules, from a binary assembly.
	/// </summary>
	public interface IAssemblyInspector
	{
		/// <summary>
		/// Examines assembly reflection info and creates an InspectedAssembly object.
		/// </summary>
		/// <param name="assembly">The assembly to be reflected.</param>
		/// <returns>Loaded assembly info</returns>
		InspectedAssembly Inspect(OpaqueAssembly assembly);
	}
}

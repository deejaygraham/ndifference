namespace NDifference
{
	/// <summary>
	/// Filters etc related to file searches. 
	/// </summary>
	public static class FileFilterConstants
	{
		public static readonly string AllFilesFilter = "*.*";
		public static readonly string AssemblyFilter = "*.dll";

		public static readonly string AssemblyFileDialogFilter = "Assembly Files (*.dll)|*.dll|All Files (*.*)|*.*";

		public static readonly string OutputFilter = "*.*ml";

		public static readonly string ProjectFileExtension = ".ndiffproj";

		public static readonly string ProjectFileDialogFilter = "NDifference Project Files (*.ndiffproj)|*.ndiffproj|All Files (*.*)|*.*";
	}
}

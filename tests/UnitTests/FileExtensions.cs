
namespace NDifference.UnitTests
{
	public static class FileExtensions
	{
		public static void DeleteIfExists(this string fileName)
		{
			if (System.IO.File.Exists(fileName))
			{
				System.IO.File.Delete(fileName);
			}
		}

		public static string GetCompanionFile(this string fullName, string extension)
		{
			string folder = System.IO.Path.GetDirectoryName(fullName);
			string name = System.IO.Path.GetFileNameWithoutExtension(fullName);
			return System.IO.Path.Combine(folder, name + extension);
		}
	}
}

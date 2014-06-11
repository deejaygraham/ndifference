using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class FileMapFacts
	{
		[Fact]
		public void FileMap_LookupRelative_Gives_Name_For_File_In_Index_Folder()
		{
			FileMap map = new FileMap();

			const string Folder = "C:\\MyFolder";

			const string key = "12345";

			map.IndexFolder = new PhysicalFolder(Folder);
			map.Add(key, new PhysicalFile(Path.Combine(Folder, "Summary.txt")));

			Assert.Equal("Summary.txt", map.LookupRelative(key));
		}

		[Fact]
		public void FileMap_LookupRelative_Gives_Relative_Path_For_File_In_Sub_Folder()
		{
			FileMap map = new FileMap();

			const string Folder = "C:\\MyFolder";

			const string key = "12345";

			map.IndexFolder = new PhysicalFolder(Folder);
			map.Add(key, new PhysicalFile(Path.Combine(Folder, "SubFolder\\Details.txt")));

			Assert.Equal("SubFolder\\Details.txt", map.LookupRelative(key));
		}

		[Fact]
		public void FileMap_LookupRelativeTo_Gives_Dotted_Path_For_File_To_File_In_Parent_Folder()
		{
			FileMap map = new FileMap();

			const string Folder = "C:\\MyFolder";

			const string key1 = "12345";
			const string key2 = "67890";

			map.IndexFolder = new PhysicalFolder(Folder);
			map.Add(key1, new PhysicalFile(Path.Combine(Folder, "Summary.txt")));
			map.Add(key2, new PhysicalFile(Path.Combine(Folder, "SubFolder\\Details.txt")));

			IFolder subFolder = new PhysicalFolder(Path.Combine(Folder, "SubFolder"));

			Assert.Equal("..\\Summary.txt", map.LookupRelativeTo(key1, subFolder));
		}

	}
}

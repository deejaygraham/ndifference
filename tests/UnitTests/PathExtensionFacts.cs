using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class PathExtensionFacts
	{
		[Fact]
		public void PathExtensions_Folder_Missing_Slash_Is_Accepted()
		{
			string baseFolder = "C:\\MyDocuments\\";
			string absolutePath = "C:\\MyDocuments\\Hello.world";

			Assert.Equal("Hello.world", baseFolder.MakeRelativePath(absolutePath));
		}

		[Fact]
		public void PathExtensions_File_In_Folder_Is_Relative_To_Folder()
		{
			string baseFolder = "C:\\MyDocuments\\";
			string absolutePath = "C:\\MyDocuments\\Hello.world";

			Assert.Equal("Hello.world", baseFolder.MakeRelativePath(absolutePath));
		}

		[Fact]
		public void PathExtensions_File_In_SubFolder_Is_Relative_To_Folder()
		{
			string baseFolder = "C:\\MyDocuments\\";
			string absolutePath = "C:\\MyDocuments\\Hello\\World.txt";

			Assert.Equal("Hello\\World.txt", baseFolder.MakeRelativePath(absolutePath));
		}

		[Fact]
		public void PathExtensions_File_In_Different_Child_Folder_Is_Relative_To_Folder()
		{
			string baseFolder = "C:\\MyDocuments\\World\\";
			string absolutePath = "C:\\MyDocuments\\Hello\\World.txt";

			Assert.Equal("..\\Hello\\World.txt", baseFolder.MakeRelativePath(absolutePath));
		}

		[Fact]
		public void PathExtensions_File_In_Parent_Folder_Is_Relative_To_Child()
		{
			string baseFolder = "C:\\MyDocuments\\Summary.txt";
			string absolutePath = "C:\\MyDocuments\\Hello\\World.txt";

			Assert.Equal("..\\Summary.txt", absolutePath.MakeRelativePath(baseFolder));
		}

	}
}

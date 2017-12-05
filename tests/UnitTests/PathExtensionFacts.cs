using System;
using System.IO;
using Xunit;

namespace NDifference.UnitTests
{
	public class PathExtensionFacts
	{
		[Fact]
		public void PathExtensions_File_In_Folder_Is_Relative_To_Folder()
		{
			const string filename = "Hello.world";

			string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string fileInFolder = Path.Combine(folder, filename);

			Assert.Equal("Hello.world", fileInFolder.MakeRelativeToFolder(folder));
		}

		[Fact]
		public void PathExtensions_File_In_SubFolder_Is_Relative_To_Folder()
		{
			const string filename = "World.txt";

			string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string absolutePath = Path.Combine(baseFolder, "Hello", filename);

			Assert.Equal(@"Hello\World.txt", absolutePath.MakeRelativeToFolder(baseFolder));
		}

		[Fact]
		public void PathExtensions_File_In_Different_Child_Folder_Is_Relative_To_Folder()
		{
			const string filename = "World.txt";

            string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            string firstFolder = Path.Combine(baseFolder, "Hello");
            string secondFolder = Path.Combine(baseFolder, "Hi");

            string fileInChild = Path.Combine(firstFolder, filename);

			Assert.Equal(@"..\Hello\World.txt", fileInChild.MakeRelativeToFolder(secondFolder));
		}

		[Fact]
		public void PathExtensions_File_In_Parent_Folder_Is_Relative_To_Child()
		{
			string baseFolder = @"C:\MyDocuments\Summary.txt";
			string absolutePath = @"C:\MyDocuments\Hello\World.txt";

			Assert.Equal(@"..\Summary.txt", baseFolder.MakeRelativeToFolder(Path.GetDirectoryName(absolutePath)));
		}
	}
}

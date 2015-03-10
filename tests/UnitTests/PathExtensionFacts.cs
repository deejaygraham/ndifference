using System;
using System.IO;
using Xunit;

namespace NDifference.UnitTests
{
    public class PathExtensionFacts
    {
        //[Fact]
        //public void PathExtensions_File_In_Folder_Is_Relative_To_Folder()
        //{
        //    const string filename = "Hello.world";

        //    string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //    string absolutePath = Path.Combine(baseFolder, filename);

        //    string expectedRelativePath = "Hello.world";
        //    Assert.Equal(expectedRelativePath, baseFolder.MakeRelativePath(absolutePath));
        //}

        //[Fact]
        //public void PathExtensions_File_In_SubFolder_Is_Relative_To_Folder()
        //{
        //    const string filename = "World.txt";

        //    string baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Hello");
        //    string absolutePath = Path.Combine(baseFolder, filename);

        //    string expectedRelativePath = Path.Combine("Hello", filename);
        //    Assert.Equal(expectedRelativePath, baseFolder.MakeRelativePath(absolutePath));
        //}

        //[Fact]
        //public void PathExtensions_File_In_Different_Child_Folder_Is_Relative_To_Folder()
        //{
        //    const string filename = "World.txt";

        //    string baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Hello");
        //    string absolutePath = Path.Combine(baseFolder, filename);

        //    string expectedRelativePath = string.Format("..{0}Hello{0}World.txt", Path.DirectorySeparatorChar);
        //    Assert.Equal(expectedRelativePath, baseFolder.MakeRelativePath(absolutePath));
        //}

        //[Fact]
        //public void PathExtensions_File_In_Parent_Folder_Is_Relative_To_Child()
        //{
        //    string baseFolder = string.Format("C:{0}MyDocuments{0}Summary.txt", Path.DirectorySeparatorChar);
        //    string absolutePath = string.Format("C:{0}MyDocuments{0}Hello{0}World.txt", Path.DirectorySeparatorChar);

        //    string expectedRelativePath = string.Format("..{0}Summary.txt", Path.DirectorySeparatorChar);

        //    Assert.Equal(expectedRelativePath, absolutePath.MakeRelativePath(baseFolder));
        //}
    }
}

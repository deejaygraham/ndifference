using System;
using System.Collections.Generic;
using System.IO;
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
            string baseFolder = string.Format("C:{0}MyDocuments{0}", Path.DirectorySeparatorChar);
            string absolutePath = string.Format("C:{0}MyDocuments{0}Hello.world", Path.DirectorySeparatorChar);

            string expectedRelativePath = "Hello.world";
            Assert.Equal(expectedRelativePath, baseFolder.MakeRelativePath(absolutePath));
        }

        [Fact]
        public void PathExtensions_File_In_Folder_Is_Relative_To_Folder()
        {
            string baseFolder = string.Format("C:{0}MyDocuments{0}", Path.DirectorySeparatorChar);
            string absolutePath = string.Format("C:{0}MyDocuments{0}Hello.world", Path.DirectorySeparatorChar);

            string expectedRelativePath = "Hello.world";
            Assert.Equal(expectedRelativePath, baseFolder.MakeRelativePath(absolutePath));
        }

        [Fact]
        public void PathExtensions_File_In_SubFolder_Is_Relative_To_Folder()
        {
            string baseFolder = string.Format("C:{0}MyDocuments{0}", Path.DirectorySeparatorChar);
            string absolutePath = string.Format("C:{0}MyDocuments{0}Hello{0}World.txt", Path.DirectorySeparatorChar);

            string expectedRelativePath = string.Format("Hello{0}World.txt", Path.DirectorySeparatorChar);
            Assert.Equal(expectedRelativePath, baseFolder.MakeRelativePath(absolutePath));
        }

        [Fact]
        public void PathExtensions_File_In_Different_Child_Folder_Is_Relative_To_Folder()
        {
            string baseFolder = string.Format("C:{0}MyDocuments{0}World{0}", Path.DirectorySeparatorChar);
            string absolutePath = string.Format("C:{0}MyDocuments{0}Hello{0}World.txt", Path.DirectorySeparatorChar);

            string expectedRelativePath = string.Format("..{0}Hello{0}World.txt", Path.DirectorySeparatorChar);
            Assert.Equal(expectedRelativePath, baseFolder.MakeRelativePath(absolutePath));
        }

        [Fact]
        public void PathExtensions_File_In_Parent_Folder_Is_Relative_To_Child()
        {
            string baseFolder = string.Format("C:{0}MyDocuments{0}Summary.txt", Path.DirectorySeparatorChar);
            string absolutePath = string.Format("C:{0}MyDocuments{0}Hello{0}World.txt", Path.DirectorySeparatorChar);

            string expectedRelativePath = string.Format("..{0}Summary.txt", Path.DirectorySeparatorChar);

            Assert.Equal(expectedRelativePath, absolutePath.MakeRelativePath(baseFolder));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class AssemblyDiskInfoFacts
	{
		[Fact]
		public void AssemblyDiskInfo_Identically_Named_Files_Are_Equal()
		{
			AssemblyDiskInfo i1 = new AssemblyDiskInfo { Name = "First.dll" };
			AssemblyDiskInfo i2 = new AssemblyDiskInfo { Name = "First.dll" };

			Assert.Equal(i1, i2);
		}

		[Fact]
		public void AssemblyDiskInfo_File_Are_Case_Insensitive()
		{
			AssemblyDiskInfo i1 = new AssemblyDiskInfo { Name = "FIRST.dll" };
			AssemblyDiskInfo i2 = new AssemblyDiskInfo { Name = "first.dll" };

			Assert.Equal(i1, i2);
		}

		[Fact]
		public void AssemblyDiskInfo_Differently_Named_Files_Are_Not_Equal()
		{
			AssemblyDiskInfo i1 = new AssemblyDiskInfo { Name = "First.dll" };
			AssemblyDiskInfo i2 = new AssemblyDiskInfo { Name = "Second.dll" };

			Assert.NotEqual(i1, i2);
		}

		[Fact]
		public void AssemblyDiskInfo_Equality_Files_Are_Equal()
		{
			AssemblyDiskInfo i1 = new AssemblyDiskInfo { Name = "First.dll" };
			AssemblyDiskInfo i2 = new AssemblyDiskInfo { Name = "first.dll" };

			Assert.True(i1 == i2);
		}


		[Fact]
		public void AssemblyDiskInfo_Equality_Files_Are_Not_Equal()
		{
			AssemblyDiskInfo i1 = new AssemblyDiskInfo { Name = "First.dll" };
			AssemblyDiskInfo i2 = new AssemblyDiskInfo { Name = "Second.dll" };

			Assert.False(i1 == i2);
		}

		[Fact]
		public void AssemblyDiskInfo_InEquality_Files_Are_Equal()
		{
			AssemblyDiskInfo i1 = new AssemblyDiskInfo { Name = "First.dll" };
			AssemblyDiskInfo i2 = new AssemblyDiskInfo { Name = "first.dll" };

			Assert.False(i1 != i2);
		}

		[Fact]
		public void AssemblyDiskInfo_InEquality_Files_Are_Not_Equal()
		{
			AssemblyDiskInfo i1 = new AssemblyDiskInfo { Name = "First.dll" };
			AssemblyDiskInfo i2 = new AssemblyDiskInfo { Name = "Second.dll" };

			Assert.True(i1 != i2);
		}
	}
}

using NDifference.Inspection;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NDifference.UnitTests
{
	public class ICombinedAssembliesFacts
	{
		[Fact]
		public void CombinedAssemblies_BuildFrom_Identical_Lists_All_Pairs_Are_Matched()
		{
			var first = new List<IAssemblyDiskInfo>();
			first.Add(new AssemblyDiskInfo { Name = "First.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			var second = new List<IAssemblyDiskInfo>();
			second.Add(new AssemblyDiskInfo { Name = "First.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			Assert.Empty(CombinedAssemblyModel.BuildFrom(first, second).InEarlierOnly);
			Assert.Empty(CombinedAssemblyModel.BuildFrom(first, second).InLaterOnly);
			Assert.Equal(3, CombinedAssemblyModel.BuildFrom(first, second).InCommon.Count());
		}

		[Fact]
		public void CombinedAssemblies_BuildFrom_Removed_Types_First_Of_Pairs_Are_Unmatched()
		{
			var first = new List<IAssemblyDiskInfo>();
			first.Add(new AssemblyDiskInfo { Name = "First.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			var second = new List<IAssemblyDiskInfo>();
			second.Add(new AssemblyDiskInfo { Name = "Third.dll" });

            Assert.Equal(2, CombinedAssemblyModel.BuildFrom(first, second).InEarlierOnly.Count());
			Assert.Empty(CombinedAssemblyModel.BuildFrom(first, second).InLaterOnly);
			Assert.Single(CombinedAssemblyModel.BuildFrom(first, second).InCommon);
		}


		[Fact]
		public void CombinedAssemblies_BuildFrom_Added_Types_Second_Of_Pairs_Are_Unmatched()
		{
			var first = new List<IAssemblyDiskInfo>();
			first.Add(new AssemblyDiskInfo { Name = "First.dll" });

			var second = new List<IAssemblyDiskInfo>();
			second.Add(new AssemblyDiskInfo { Name = "First.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Third.dll" });

            Assert.Empty(CombinedAssemblyModel.BuildFrom(first, second).InEarlierOnly);
			Assert.Equal(2, CombinedAssemblyModel.BuildFrom(first, second).InLaterOnly.Count());
			Assert.Single(CombinedAssemblyModel.BuildFrom(first, second).InCommon);
		}

	}
}

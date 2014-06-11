using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class PhysicalFileFacts
	{
		[Fact]
		public void PhysicalFile_Parent_Relative_To_Child_Is_Relative()
		{
			IFile parent = new PhysicalFile("C:\\Reports\\Summary.html");
			IFile child = new PhysicalFile("C:\\Reports\\Sub\\Detail.html");

			Assert.Equal("Sub\\Detail.html", child.RelativeTo(parent));
		}

		[Fact]
		public void PhysicalFile_Child_Relative_To_Parent_Is_Relative()
		{
			IFile parent = new PhysicalFile("C:\\Reports\\Summary.html");
			IFile child = new PhysicalFile("C:\\Reports\\Sub\\Detail.html");

			Assert.Equal("..\\Summary.html", parent.RelativeTo(child));
		}
	}
}

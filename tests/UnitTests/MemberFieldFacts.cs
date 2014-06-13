using NDifference.TypeSystem;
using Xunit;

namespace NDifference.UnitTests
{
	public class MemberFieldFacts
	{
		[Fact]
		public void FieldDeclaration_ToString_Is_Formatted_On_Single_Line()
		{
			var fd = new MemberField
			{
				Name = "Age",
				FieldType = new FullyQualifiedName("System.Int32")
			};

			Assert.Equal("Int32 Age ", fd.ToString());
		}

		[Fact]
		public void FieldDeclaration_ToString_ReadOnly_Formatted_On_Single_Line()
		{
			var fd = new MemberField
			{
				Name = "Age",
				FieldType = new FullyQualifiedName("System.Int32"),
				IsReadOnly = true
			};

			Assert.Equal("readonly Int32 Age ", fd.ToString());
		}

		[Fact]
		public void FieldDeclaration_ToString_Static_Formatted_On_Single_Line()
		{
			var fd = new MemberField
			{
				Name = "Age",
				FieldType = new FullyQualifiedName("System.Int32"),
				IsStatic = true
			};

			Assert.Equal("static Int32 Age ", fd.ToString());
		}
	}
}

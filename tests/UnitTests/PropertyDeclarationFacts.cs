using NDifference.TypeSystem;
using Xunit;

namespace NDifference.UnitTests
{
	public class PropertyDeclarationFacts
	{
		[Fact]
		public void PropertyDeclaration_ToString_Is_Code_Formatted_On_Single_Line()
		{
			var pd = new PropertyDeclaration
			{
				Name = "Age",
				Type = new FullyQualifiedName("System.Int32"),
				HasGetter = true,
				HasSetter = true
			};

			Assert.Equal("Int32 Age { get; set; } ", pd.ToString());

		}
	}
}

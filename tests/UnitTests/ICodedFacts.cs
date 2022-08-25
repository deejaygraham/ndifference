using NDifference.SourceFormatting;
using NDifference.TypeSystem;
using Xunit;

namespace NDifference.UnitTests
{
    public class ICodedFacts
	{
		[Fact]
		public void ICoded_Flattens_To_Xml()
		{
			FullyQualifiedName returnType = FullyQualifiedNameBuilder.Type()
				.Named("TestClass")
				.InNamespace("MyLib")
				.Build();

			ICoded code = returnType.ToCode();

			string flat = code.ToXml();

			Assert.Contains("<SourceCode>", flat);
			Assert.Contains("<tn>MyLib.TestClass</tn>", flat);
			Assert.Contains("</SourceCode>", flat);
		}

		[Fact]
		public void ICoded_Flattens_To_PlainText()
		{
			var type = FullyQualifiedNameBuilder.Type()
				.Named("TestClass")
				.InNamespace("MyLib")
				.Build();

			Assert.Equal("MyLib.TestClass", type.ToCode().ToPlainText());
		}

		[Fact]
		public void ICoded_FullyQualifiedName_Escapes_Generic_Markers()
		{
			var list = FullyQualifiedNameBuilder.Type()
				.Named("List<string>")
				.InNamespace("System.Collections.Generic")
				.Build();

			Assert.Equal("System.Collections.Generic.List<string>", list.ToCode().ToPlainText());
		}

		[Fact]
		public void SourceCode_Equals_Returns_True_For_Identical_Types()
		{
			SourceCode thisCode = new SourceCode();
			thisCode.Add(new TypeNameTag("ThisIsMyType"));

			SourceCode thatCode = new SourceCode();
			thatCode.Add(new TypeNameTag("ThisIsMyType"));

			Assert.Equal(thisCode, thatCode);
		}

		[Fact]
		public void SourceCode_Equals_Returns_False_For_NonIdentical_Types()
		{
			SourceCode thisCode = new SourceCode();
			thisCode.Add(new TypeNameTag("ThisIsMyType"));

			SourceCode thatCode = new SourceCode();
			thatCode.Add(new TypeNameTag("ThisIsNotMyType"));

			Assert.NotEqual(thisCode, thatCode);
		}
	}
}

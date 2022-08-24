using NDifference.Reporting;
using System;
using Xunit;

namespace NDifference.UnitTests
{
	public class ReportAsMarkdownFacts
	{
		[Fact]
		public void ReportAsMarkdown_Supports_Markdown_Format_Output()
		{
			var format = new ReportAsMarkdown();

			Assert.True(format.Supports("markdown"));
			Assert.True(format.Supports(".md"));
		}

		[Fact]
		public void ReportAsMarkdown_Throws_Exception_For_Bad_Title_Size()
        {
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				new ReportAsMarkdown().FormatTitle(7, "Hello", "first");
			});
		}

		[Fact]
		public void ReportAsMarkdown_Creates_Hashes_For_Title_Size()
		{
			Assert.StartsWith("\r\n\r\n### ", new ReportAsMarkdown().FormatTitle(3, "Hello", null));
		}

		[Fact]
		public void ReportAsMarkdown_Adds_Id_To_Title()
		{
			Assert.Contains("{ #HelloChunk }", new ReportAsMarkdown().FormatTitle(3, "Hello", "HelloChunk"));
		}

		[Fact]
		public void ReportAsMarkdown_Separates_Table_Headers_By_Pipes()
		{
			string line = new ReportAsMarkdown().FormatTableHeader(new string[] { "Name", "Value" });

			Assert.Equal("\r\n| Name | Value |\r\n|------|-------|\r\n", line);
		}

		[Fact]
		public void ReportAsMarkdown_Separates_Table_Cells_By_Pipes()
		{
			string line = new ReportAsMarkdown().FormatTableRow(new string[] { "Hello", "World" });

			Assert.Equal("| Hello | World |\r\n", line);
		}

		[Fact]
		public void ReportAsMarkdown_Creates_Markdown_Links()
		{
			string line = new ReportAsMarkdown().FormatLink("http://google.com", "Google");

			Assert.Equal("[Google](http://google.com)", line);
		}
	}
}

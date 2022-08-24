using NDifference.Reporting;
using Xunit;

namespace NDifference.UnitTests
{
	public class ReportAsHtml4Facts
	{
		[Fact]
		public void ReportAsHtml4_Supports_Html_Format_Output()
		{
			ReportAsHtml4 format = new ReportAsHtml4();

			Assert.True(format.Supports("html"));
			Assert.True(format.Supports(".HTML"));
		}

	}
}

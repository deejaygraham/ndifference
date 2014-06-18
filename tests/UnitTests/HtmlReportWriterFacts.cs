using NDifference.Analysis;
using NDifference.Framework;
using NDifference.Projects;
using NDifference.Reporting;
using System.Linq;
using Xunit;

namespace NDifference.UnitTests
{
	public class HtmlReportWriterFacts
	{
		[Fact]
		public void HtmlReportWriter_Summary_Contains_Basic_Html_Structure()
		{
			HtmlReportWriter writer = new HtmlReportWriter();

			var superficial = new IdentifiedChangeCollection();

			var output = new InMemoryReportOutput();

			var project = ProjectBuilder.Default();
			project.Product.Clear();

			project.Product.Name = "Example";
			project.Product.Add(new ProductIncrement { Name = "1.0" });
			project.Product.Add(new ProductIncrement { Name = "2.0" });

			writer.Write(superficial, output, writer.SupportedFormats.First());

			Assert.Contains("<html>", output.Content);

			Assert.Contains("<head>", output.Content);
			Assert.Contains("</head>", output.Content);

			Assert.Contains("<body>", output.Content);
			Assert.Contains("</body>", output.Content);

			Assert.Contains("</html>", output.Content);
		}

		[Fact]
		public void HtmlReportWriter_Summary_Contains_Assembly_Structure()
		{
			HtmlReportWriter writer = new HtmlReportWriter();

			var superficial = new IdentifiedChangeCollection();
			superficial.Add(new IdentifiedChange { Description = @"C:\\Test.dll" });

			var output = new InMemoryReportOutput();

			var project = ProjectBuilder.Default();
			project.Product.Clear();

			project.Product.Name = "Example";
			project.Product.Add(new ProductIncrement { Name = "1.0" });
			project.Product.Add(new ProductIncrement { Name = "2.0" });

			writer.Write(superficial, output, writer.SupportedFormats.First());

			Assert.Contains("Test.dll", output.Content);
		}
	}
}

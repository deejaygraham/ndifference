using NDifference;
using NDifference.Analysis;
using NDifference.Framework;
using NDifference.Inspectors;
using NDifference.Plugins;
using NDifference.Projects;
using NDifference.Reflection;
using NDifference.Reporting;
using System;
using System.IO;

namespace WalkingSkeleton
{
	public static class Program
	{
		public static int Main(string[] args)
		{
			if (args.Length != 3)
			{
				Console.Error.WriteLine(
					"Usage: WalkingSkeleton <previous-release-folder> <current-release-folder> <output-folder>");
				return 1;
			}

			string previousFolder = Path.GetFullPath(args[0]);
			string currentFolder = Path.GetFullPath(args[1]);
			string outputFolder = Path.GetFullPath(args[2]);

			if (!Directory.Exists(previousFolder))
			{
				Console.Error.WriteLine("Previous release folder does not exist: {0}", previousFolder);
				return 1;
			}

			if (!Directory.Exists(currentFolder))
			{
				Console.Error.WriteLine("Current release folder does not exist: {0}", currentFolder);
				return 1;
			}

			Directory.CreateDirectory(outputFolder);

			var infoBuilder = new AssemblyDiskInfoBuilder();
			var previousVersion = new ProductIncrement
			{
				Name = new DirectoryInfo(previousFolder).Name
			};
			previousVersion.AddRange(infoBuilder.BuildFromFolder(previousFolder));

			var currentVersion = new ProductIncrement
			{
				Name = new DirectoryInfo(currentFolder).Name
			};
			currentVersion.AddRange(infoBuilder.BuildFromFolder(currentFolder));

			var project = ProjectBuilder.Default();
			project.Product.Clear();
			project.Product.Name = "Command-line comparison";
			project.Product.Add(previousVersion);
			project.Product.Add(currentVersion);
			project.Settings.OutputFolder = outputFolder;

			IFileFinder builtInInspectorFinder = new ExplicitFileFinder(
				typeof(RuntimeInspector).Assembly.Location);
			IFileFinder customPluginFinder = PluginDirectory
				.ForCurrentUser()
				.CreateFinder();
			var progress = new ConsoleProgress();

			var inspectors = new InspectorRepository();
			inspectors.Find(builtInInspectorFinder);
			inspectors.Find(customPluginFinder);
			inspectors.Filter(new InspectorFilter(project.Settings.IgnoreInspectors));

			IAnalysisWorkflow analysis = new AnalysisWorkflow(
				builtInInspectorFinder,
				new CecilReflectorFactory());
			AnalysisResult result = analysis.RunAnalysis(project, inspectors, progress);

			if (result.Cancelled)
			{
				Console.Error.WriteLine("Analysis was cancelled.");
				return 1;
			}

			IReportingRepository reports = new ReportingRepository();
			reports.Find(new ExplicitFileFinder(typeof(HtmlReportWriter).Assembly.Location));
			reports.Find(customPluginFinder);

			IReportingWorkflow reporting = new ReportingWorkflow();
			reporting.RunReports(project, reports, result, progress);

			Console.WriteLine("Report written to {0}", project.Settings.SuggestIndexPath(".md"));
			return 0;
		}

		private sealed class ConsoleProgress : IProgress<NDifference.Progress>
		{
			public void Report(NDifference.Progress value)
			{
				if (value != null && !string.IsNullOrWhiteSpace(value.Description))
				{
					Console.WriteLine(value.Description);
				}
			}
		}
	}
}

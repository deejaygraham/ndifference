using NDifference.Analysis;
using NDifference.Projects;
using System;
using System.IO;
using System.Linq;

namespace NDifference.Reporting
{
	public class ReportingWorkflow : IReportingWorkflow
	{
		public void RunReports(Project project, IReportingRepository reportWriters, AnalysisResult results)
		{
			try
			{
				var writer = reportWriters.Find("html");

				if (writer != null)
				{
					IReportFormat format = writer.SupportedFormats.First();

					var builder = FileMapBuilder.Map()
						.UsingProject(project)
						.As(format)
						.WithIndex(results.Summary.Identifier)
						.With(results.Summary);

					builder.With(results.AssemblyLevelChanges);

					if (!project.Settings.ConsolidateAssemblyTypes)
						builder.With(results.TypeLevelChanges);

					writer.Map = builder.Build();

					if (!String.IsNullOrEmpty(project.Settings.SubFolder))
					{
						if (Directory.Exists(project.Settings.SubPath))
						{
							Directory.GetFiles(project.Settings.SubPath, "*" + format.Extension).ToList().ForEach(x => File.Delete(x));
						}
					}

					// write in reverse order...so that files should exist when we write a link..
					foreach (var typeChange in results.TypeLevelChanges)
					{
						IReportOutput typeOutput = new FileOutput(Path.Combine(project.Settings.SubPath, typeChange.Name.HtmlSafeTypeName() + format.Extension));

						writer.Write(typeChange, typeOutput, format);
					}

					foreach (var dllChange in results.AssemblyLevelChanges)
					{
						IReportOutput dllOutput = new FileOutput(Path.Combine(project.Settings.SubPath, dllChange.Name + format.Extension));

						writer.Write(dllChange, dllOutput, format);
					}

					// finally write out summary...
					IReportOutput output = new FileOutput(Path.Combine(project.Settings.OutputFolder, project.Settings.IndexName + format.Extension));

					writer.Write(results.Summary, output, format);
				}
			}
			finally
			{
			}
		}
	}
}

using NDifference.Analysis;
using NDifference.Framework;
using NDifference.Projects;
using System;
using System.IO;
using System.Linq;

namespace NDifference.Reporting
{
	public class ReportingWorkflow : IReportingWorkflow
	{
		public event EventHandler<CancellableEventArgs> ReportsStarting;

		public event EventHandler<FileProgessEventArgs> ReportStarting;

		public event EventHandler ReportComplete;

		public event EventHandler ReportsComplete;

		public void RunReports(Project project, IReportingRepository reportWriters, AnalysisResult results)
		{
			try
			{
				//project.Settings.ConsolidateAssemblyTypes = true;

				var cancellable = new CancellableEventArgs();
				this.ReportsStarting.Fire(this, cancellable);

				if (cancellable.CancelAction)
				{
					return;
				}

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

					if (!project.Settings.ConsolidateAssemblyTypes)
					{
						// write in reverse order...so that files should exist when we write a link..
						foreach (var typeChange in results.TypeLevelChanges)
						{
							IReportOutput typeOutput = new FileOutput(Path.Combine(project.Settings.SubPath, typeChange.Name.HtmlSafeTypeName() + format.Extension));

							this.ReportStarting.Fire(this, new FileProgessEventArgs { FileName = typeChange.Name });

							writer.Write(typeChange, typeOutput, format);

							this.ReportComplete.Fire(this);
						}
					}

					foreach (var dllChange in results.AssemblyLevelChanges)
					{
						IReportOutput dllOutput = new FileOutput(Path.Combine(project.Settings.SubPath, dllChange.Name + format.Extension));

						this.ReportStarting.Fire(this, new FileProgessEventArgs { FileName = dllChange.Name });

						writer.Write(dllChange, dllOutput, format);

						// need to write out everything relating to this assembly in one place...

						//if (project.Settings.ConsolidateAssemblyTypes)
						//{
						//	foreach (var typeChange in results.TypeLevelChanges.Where(x => x.Changes.)
						//	{
						//		writer.Write(typeChange, dllOutput, format);
						//	}
						//}

						this.ReportComplete.Fire(this);
					}

					// finally write out summary...
					IReportOutput output = new FileOutput(Path.Combine(project.Settings.OutputFolder, project.Settings.IndexName + format.Extension));

					this.ReportStarting.Fire(this, new FileProgessEventArgs { FileName = project.Settings.IndexName });

					writer.Write(results.Summary, output, format);

					this.ReportComplete.Fire(this);
				}
			}
			finally
			{
				this.ReportsComplete.Fire(this);
			}
		}
	}
}

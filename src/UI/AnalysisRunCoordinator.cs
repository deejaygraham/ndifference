using NDifference.Analysis;
using NDifference.Framework;
using NDifference.Inspectors;
using NDifference.Plugins;
using NDifference.Projects;
using NDifference.Reflection;
using NDifference.Reporting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NDifference.UI
{
	internal sealed class AnalysisRunCoordinator : IDisposable
	{
		private readonly PluginCatalog pluginCatalog;
		private readonly AnalysisWorkflow analysisWorkflow;
		private readonly ReportingWorkflow reportingWorkflow;

		public AnalysisRunCoordinator()
		{
			IFileFinder builtInInspectorFinder =
				new ExplicitFileFinder(typeof(RuntimeInspector).Assembly.Location);

			this.pluginCatalog = new PluginCatalog(
				builtInInspectorFinder,
				new ExplicitFileFinder(typeof(HtmlReportWriter).Assembly.Location),
				PluginDirectory.ForCurrentUser().CreateFinder());
			this.analysisWorkflow = new AnalysisWorkflow(
				builtInInspectorFinder,
				new CecilReflectorFactory());
			this.reportingWorkflow = new ReportingWorkflow();
		}

		public InspectorRepository CreateInspectorRepository()
		{
			return this.pluginCatalog.CreateInspectorRepository();
		}

		public Task RunAsync(
			Project project,
			IProgress<Progress> progress,
			CancellationToken cancellationToken)
		{
			if (project == null)
			{
				throw new ArgumentNullException(nameof(project));
			}

			if (progress == null)
			{
				throw new ArgumentNullException(nameof(progress));
			}

			return Task.Run(
				() => this.Run(project, progress, cancellationToken),
				cancellationToken);
		}

		public void Dispose()
		{
			this.pluginCatalog.Dispose();
		}

		private void Run(
			Project project,
			IProgress<Progress> progress,
			CancellationToken cancellationToken)
		{
			EventHandler<CancellableEventArgs> cancelWhenRequested =
				(sender, args) =>
					args.CancelAction = cancellationToken.IsCancellationRequested;

			this.analysisWorkflow.AnalysisStarting += cancelWhenRequested;
			this.analysisWorkflow.AssemblyComparisonStarting += cancelWhenRequested;
			this.analysisWorkflow.TypeComparisonStarting += cancelWhenRequested;
			this.reportingWorkflow.ReportsStarting += cancelWhenRequested;

			try
			{
				progress.Report(new Progress("Starting..."));
				progress.Report(new Progress("Loading Plugins..."));

				InspectorRepository inspectors =
					this.pluginCatalog.CreateInspectorRepository();
				inspectors.Filter(
					new InspectorFilter(project.Settings.IgnoreInspectors));

				progress.Report(new Progress("Starting Analysis..."));

				AnalysisResult result = this.analysisWorkflow.RunAnalysis(
					project,
					inspectors,
					progress);

				if (result.Cancelled)
				{
					throw new OperationCanceledException(cancellationToken);
				}

				cancellationToken.ThrowIfCancellationRequested();

				progress.Report(new Progress("Starting Reports..."));
				this.reportingWorkflow.RunReports(
					project,
					this.pluginCatalog.CreateReportingRepository(),
					result,
					progress);

				cancellationToken.ThrowIfCancellationRequested();
			}
			finally
			{
				this.analysisWorkflow.AnalysisStarting -= cancelWhenRequested;
				this.analysisWorkflow.AssemblyComparisonStarting -= cancelWhenRequested;
				this.analysisWorkflow.TypeComparisonStarting -= cancelWhenRequested;
				this.reportingWorkflow.ReportsStarting -= cancelWhenRequested;
			}
		}
	}
}

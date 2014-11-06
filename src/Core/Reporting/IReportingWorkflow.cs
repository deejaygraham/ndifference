using NDifference.Analysis;
using NDifference.Framework;
using NDifference.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public interface IReportingWorkflow
	{
		event EventHandler<CancellableEventArgs> ReportsStarting;

		event EventHandler<FileProgessEventArgs> ReportStarting;

		event EventHandler ReportComplete;

		event EventHandler ReportsComplete;

		void RunReports(Project project, IReportingRepository reporters, AnalysisResult results, IProgress<ProgressValue> progressIndicator);
	}
}

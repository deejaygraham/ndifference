using NDifference.Analysis;
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
		void RunReports(Project project, IReportingRepository reporters, AnalysisResult results);
	}
}

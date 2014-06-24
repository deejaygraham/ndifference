using NDifference.Framework;
using NDifference.Inspectors;
using NDifference.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Analysis
{
	public interface IAnalysisWorkflow
	{
		event EventHandler<CancellableEventArgs> AnalysisStarting;

		event EventHandler AnalysisComplete;

		event EventHandler<CancellableEventArgs> AssemblyComparisonStarting;

		event EventHandler AssemblyComparisonComplete;

		event EventHandler<CancellableEventArgs> TypeComparisonStarting;

		event EventHandler TypeComparisonComplete;

		AnalysisResult RunAnalysis(Project project, InspectorRepository inspectors);
	}
}

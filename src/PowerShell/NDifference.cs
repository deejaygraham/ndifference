using NDifference.Analysis;
using NDifference.Framework;
using NDifference.Inspectors;
using NDifference.Projects;
using NDifference.Reflection;
using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "ApiDifferences", SupportsShouldProcess = true)]
    //[OutputType(new Type[]
    //{
    //    typeof(FortuneCookie)
    //})]
    public class GetDifference : Cmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "Path to the reference assembly (old version) to compare against")]
        public string Reference { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Path to the assembly (new version) to compare against reference", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            WriteDebug("Starting API Difference Cmdlet");

            try
            {
                Project project = ProjectBuilder.Default();

                project.Product.Name = "Untitled";
                ProductIncrement source = project.Product[0];
                source.Name = "Source";
                source.Add(AssemblyDiskInfoBuilder.BuildFromFile(this.Reference));

                ProductIncrement target = project.Product[1];
                target.Name = "Target";
                target.Add(AssemblyDiskInfoBuilder.BuildFromFile(this.Path));

                // temporary folder !!!
                // project.Settings.OutputFolder = this.OutputFolder.GetFullPath();

                IProgress<Progress> progressIndicator = new Progress<Progress>(value =>
                {
                    if (!String.IsNullOrEmpty(value.Description))
                    {
                        WriteVerbose(value.Description);
                    }
                });

                System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
                {
                    progressIndicator.Report(new Progress("Starting..."));

                    IFileFinder finder = new FileFinder(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), FileFilterConstants.AssemblyFilter);

                    AnalysisWorkflow analysis = new AnalysisWorkflow(
                        finder,
                        new CecilReflectorFactory());

                    progressIndicator.Report(new Progress("Loading Plugins..."));

                    InspectorRepository ir = new InspectorRepository();
                    ir.Find(finder);

                    InspectorFilter filter = new InspectorFilter(project.Settings.IgnoreInspectors);

                    ir.Filter(filter);

                    progressIndicator.Report(new Progress("Starting Analysis..."));

                    var result = analysis.RunAnalysis(project, ir, progressIndicator);

                    IReportingRepository rr = new ReportingRepository();
                    rr.Find(finder);

                    IReportingWorkflow reporting = new ReportingWorkflow();

                    progressIndicator.Report(new Progress("Starting Reports..."));

                    reporting.RunReports(project, rr, result, progressIndicator);

                    //WriteObject(cookies[0]);
                });

                System.Threading.Tasks.Task t2 = t.ContinueWith((antecedent) =>
                {
                    WriteVerbose("Analysis Complete.");

                }, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());

                t.Start();
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(ex, "ND001", ErrorCategory.OperationStopped, null));
            }
        }
    }
}

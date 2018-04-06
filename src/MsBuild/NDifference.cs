using Microsoft.Build.Framework;
using NDifference.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using NDifference.Analysis;
using NDifference.Reflection;
using NDifference.Inspectors;
using NDifference.Reporting;
using Microsoft.Build.Utilities;
using NDifference.Framework;

namespace NDifference.Tasks
{
	/// <summary>
	/// MsBuild task.
	/// Example:
	/// <UsingTask AssemblyFile="$(MSBuildExtensionsPath)\NDifference.Tasks.dll"
	///			TaskName="NDifference"/>
	///			
	/// Specify either the project file.
	/// <Target Name="Demo">
	///		
	///		<NDifference 
	///			ProjectFile="MyProject.ndiff" 
	///			 />
	///	</Target>
	///	OR 
	/// <Target Name="Demo">
	///		
	///		<NDifference 
	///			ProductName="My Project"
	///			SourceName="2011"
	///			SourceAssemblies="@(FromDlls)"
	///			TargetName="2012"
	///			TargetAssemblies="@(ToDlls)"
	///			OutputFolder=""
	///			/>
	///	</Target>
	/// </summary>
	public class NDifference : Task
	{
		public ITaskItem ProjectFile { get; set; }

		public string ProductName { get; set; }

		public string SourceName { get; set; }

		public ITaskItem[] SourceAssemblies { get; set; }

		public string TargetName { get; set; }
		
		public ITaskItem[] TargetAssemblies { get; set; }

		public ITaskItem OutputFolder { get; set; }

		public bool Clean { get; set; }

		public override bool Execute()
		{
			Log.LogMessage(MessageImportance.Low, "Starting API Difference Analysis");

			try
			{
				this.ValidateParameters();

				Project project = ProjectBuilder.Default();

				if (this.ProjectFile == null)
				{
					project.Product.Name = this.ProductName;
					ProductIncrement source = project.Product[0];
					source.Name = this.SourceName;

					if (this.SourceAssemblies.Length > 0)
					{
						foreach(ITaskItem sourceItem in this.SourceAssemblies)
						{
							source.Add(AssemblyDiskInfoBuilder.BuildFromFile(sourceItem.GetFullPath()));
						}
					}

					ProductIncrement target = project.Product[1];
					target.Name = this.TargetName;

					if (this.TargetAssemblies.Length > 0)
					{
						foreach (ITaskItem targetItem in this.TargetAssemblies)
						{
							target.Add(AssemblyDiskInfoBuilder.BuildFromFile(targetItem.GetFullPath()));
						}
					}

					project.Settings.OutputFolder = this.OutputFolder.GetFullPath();
				}
				else
				{
					string pathToProjectFile = this.ProjectFile.GetMetadata("FullPath");

					if (!File.Exists(pathToProjectFile))
					{
						Log.LogError("Project file \'{0}\' does not exist", pathToProjectFile);
						return false;
					}

					Log.LogMessage(MessageImportance.Low, "Loading project file {0}", pathToProjectFile);

					project = ProjectReader.LoadFromFile(pathToProjectFile);
				}

				var errors = this.ValidateSettings(project);

				if (errors.Any())
				{
					foreach (var error in errors)
					{
						Log.LogError(error);
					}

					return false;
				}

				IProgress<Progress> progressIndicator = new Progress<Progress>(value =>
				{
					if (!String.IsNullOrEmpty(value.Description))
					{
						Log.LogMessage(MessageImportance.Low, value.Description);
					}
				});

				System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
				{
					progressIndicator.Report(new Progress("Starting..."));

					IFileFinder finder = new FileFinder(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileFilterConstants.AssemblyFilter);

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
				});

				System.Threading.Tasks.Task t2 = t.ContinueWith((antecedent) =>
				{
					Log.LogMessage(MessageImportance.Low, "Analysis Complete.");

				}, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());

				t.Start();
			}
			catch (Exception ex)
			{
				Log.LogErrorFromException(ex);
			}

			return true;
		}

		private void ValidateParameters()
		{
			Log.LogMessageFromText("Validating arguments", MessageImportance.Low);

			// specify a project file or individual arguments
			bool fileSpecified = this.ProjectFile != null;

			if (fileSpecified)
			{
				this.ProjectFile.ValidateFile();
			}
			else
			{
				if (this.SourceAssemblies == null)
					throw new Exception("SourceAssemblies not set");

				if (this.SourceAssemblies.Length > 0)
				{
					this.SourceAssemblies.ValidateFiles();
				}

				if (this.TargetAssemblies == null)
					throw new Exception("SourceAssemblies not set");

				if (this.TargetAssemblies.Length > 0)
				{
					this.TargetAssemblies.ValidateFiles();
				}

				if (this.OutputFolder != null)
				{
					this.OutputFolder.ValidateFolder();
				}
			}
		}

		private List<string> ValidateSettings(Project project)
		{
			List<string> validationErrors = new List<string>();

			if (string.IsNullOrEmpty(project.Product.Name))
			{
				validationErrors.Add("Please give the project a name.");
			}

			var first = project.Product.ComparedIncrements.First;
			var second = project.Product.ComparedIncrements.Second;

			if (string.IsNullOrEmpty(first.Name))
			{
				validationErrors.Add("Please name the \"old\" version of the product.");
			}

			if (!first.Assemblies.Any())
			{
				validationErrors.Add("You have not selected an old version of your assemblies to analyse.");
			}

			if (string.IsNullOrEmpty(second.Name))
			{
				validationErrors.Add("Please name the \"new\" version of the product.");
			}

			if (!second.Assemblies.Any())
			{
				validationErrors.Add("You have not selected a new version of your assemblies to analyse.");
			}

			if (string.IsNullOrEmpty(project.Settings.OutputFolder))
			{
				validationErrors.Add("Output folder is not set.");
			}

			return validationErrors;
		}
	}
}

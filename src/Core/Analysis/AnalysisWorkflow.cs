﻿using NDifference.Inspectors;
using NDifference.Plugins;
using NDifference.Projects;
using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Analysis
{
	/// <summary>
	/// public entry point for creating an API analysis.
	/// </summary>
	public class AnalysisWorkflow : IAnalysisWorkflow
	{
		public AnalysisWorkflow(IFileFinder finder, IAssemblyReflectorFactory reflectorFactory)
		{
			Debug.Assert(finder != null, "Finder cannot be null");

			this.Finder = finder;
			this.ReflectorFactory = reflectorFactory;

			this.AssemblyCollectionInspectors = new List<IAssemblyCollectionInspector>();
			this.AssemblyInspectors = new List<IAssemblyInspector>();
			this.TypeCollectionInspectors = new List<ITypeCollectionInspector>();
			this.TypeInspectors = new List<ITypeInspector>();

			this.ReportWriters = new List<IReportWriter>();
		}

		private IFileFinder Finder { get; set; }

		private IAssemblyReflectorFactory ReflectorFactory { get; set; }

		private List<IAssemblyCollectionInspector> AssemblyCollectionInspectors { get; set; }

		private List<IAssemblyInspector> AssemblyInspectors { get; set; }

		private List<ITypeCollectionInspector> TypeCollectionInspectors { get; set; }

		private List<ITypeInspector> TypeInspectors { get; set; }

		private List<IReportWriter> ReportWriters { get; set; }

		// analysis result ???

		public void Analyse(Project project)
		{
			try
			{
				this.DiscoverPlugins();

				IdentifiedChangeCollection summaryChanges = new IdentifiedChangeCollection
				{
					Name = project.Product.Name,
					Heading = project.Settings.SummaryTitle
				};

				foreach(var aci in this.AssemblyCollectionInspectors)
				{
					aci.Inspect(project.Product.ComparedIncrements.First.Assemblies, project.Product.ComparedIncrements.Second.Assemblies, summaryChanges);
				}

				string autoGenerationMessage = String.Format("<!-- Generated by {0} {1} {2} {3} -->",
					project.Settings.ApplicationName,
					project.Settings.ApplicationVersion,
					DateTime.Now.ToLongDateString(),
					DateTime.Now.ToLongTimeString());

				summaryChanges.MetaBlocks.Add(autoGenerationMessage);

				if (String.IsNullOrEmpty(project.Settings.HeadTag))
				{
					summaryChanges.MetaBlocks.Add("<!-- No custom head content defined -->");
				}
				else
				{
					summaryChanges.MetaBlocks.Add("<!-- Custom head content -->");
					summaryChanges.MetaBlocks.Add(project.Settings.HeadTag);
					summaryChanges.MetaBlocks.Add("<!-- End of custom head content -->");
				}

				const int MaxLineLength = 80;

				if (String.IsNullOrEmpty(project.Settings.StyleTag))
				{
					summaryChanges.MetaBlocks.Add("<!-- No custom style content defined -->");
				}
				else
				{
					summaryChanges.MetaBlocks.Add("<!-- Custom style content -->");
					summaryChanges.MetaBlocks.Add(project.Settings.StyleTag.SplitLongLines(MaxLineLength));
					summaryChanges.MetaBlocks.Add("<!-- End of custom style content -->");
				}

				summaryChanges.SummaryBlocks.Add("Name", project.Product.Name);

				string fromVersion = project.Product.ComparedIncrements.First.Name;
				string toVersion = project.Product.ComparedIncrements.Second.Name;

				summaryChanges.SummaryBlocks.Add("From", fromVersion);
				summaryChanges.SummaryBlocks.Add("To", toVersion);
				summaryChanges.SummaryBlocks.Add("% Churn", "Not calculated yet");

				if (String.IsNullOrEmpty(project.Settings.FooterText))
				{
					summaryChanges.FooterBlocks.Add("<!-- No footer block defined -->");
				}
				else
				{
					summaryChanges.FooterBlocks.Add("<!-- Footer block -->");
					summaryChanges.FooterBlocks.Add(project.Settings.FooterText.SplitLongLines(MaxLineLength));
					summaryChanges.FooterBlocks.Add("<!-- End of Footer block -->");
				}

				// now get each assembly pair and compare them at the internal level...

				foreach (var common in summaryChanges.ChangesInCategory(new CategoryPriority(2)))
				{
					// description is the name of the assembly...
					var dll1 = project.Product.ComparedIncrements.First.Assemblies.FindMatchFor(common.Description);
					var dll2 = project.Product.ComparedIncrements.Second.Assemblies.FindMatchFor(common.Description);

					if (dll1 != null && dll2 != null)
					{
						// inspect each assembly...

						// introspect each one...
						var reflector1 = this.ReflectorFactory.LoadAssembly(dll1.Path);
						var reflector2 = this.ReflectorFactory.LoadAssembly(dll2.Path);

						var typesIn1 = reflector1.GetTypes(AssemblyReflectionOption.Public);
						var typesIn2 = reflector2.GetTypes(AssemblyReflectionOption.Public);

						// do all comparisons...

						// each inspector

					}
				}

				var reportRepo = new ReportingRepository();
				var reportFinder = new ReportingPluginDiscoverer(this.Finder);

				reportRepo.AddRange(reportFinder.Find());

				var writer = reportRepo.Find("html");

				if (writer != null)
				{
					IReportFormat format = writer.SupportedFormats.First();

					//writer.Map = OutputFileMapBuilder.BuildFor(summaryChanges, project, format);

					IReportOutput output = new FileOutput(Path.Combine(project.Settings.OutputFolder, project.Settings.IndexName));
					output.Folder = project.Settings.OutputFolder;

					writer.Write(summaryChanges, output, format);

					//foreach (var dllChange in assemblyLevelChanges)
					//{
					//	IReportOutput dllOutput = new FileOutput(outputPath);
					//	output.Folder = project.Settings.OutputFolder;

					//	//writer.Write(dllChange, output, format);
					//}
				}
			}
			catch
			{
				// add to list of errors.
			}
		}

		private void DiscoverPlugins()
		{
			this.AssemblyCollectionInspectors.AddRange(new AssemblyCollectionInspectorPluginDiscoverer(this.Finder).Find());
			this.AssemblyInspectors.AddRange(new AssemblyInspectorPluginDiscoverer(this.Finder).Find());
			this.TypeCollectionInspectors.AddRange(new TypeCollectionInspectorPluginDiscoverer(this.Finder).Find());
			this.TypeInspectors.AddRange(new TypeInspectorPluginDiscoverer(this.Finder).Find());

			this.ReportWriters.AddRange(new ReportingPluginDiscoverer(this.Finder).Find());
		}
	}
}

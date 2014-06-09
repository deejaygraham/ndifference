using NDifference.Inspection;
using NDifference.Inspectors;
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

				summaryChanges.MetaBlocks.AddRange(project.Settings.GenerateMetaBlocks());
				summaryChanges.FooterBlocks.AddRange(project.Settings.GenerateFooterBlocks());

				var firstVersion = project.Product.ComparedIncrements.First;
				var secondVersion = project.Product.ComparedIncrements.Second;

				RunAssemblyCollectionInspectors(firstVersion.Assemblies, secondVersion.Assemblies, summaryChanges);
				
				summaryChanges.SummaryBlocks.Add("Name", project.Product.Name);
				summaryChanges.SummaryBlocks.Add("From", firstVersion.Name);
				summaryChanges.SummaryBlocks.Add("To", secondVersion.Name);
				summaryChanges.SummaryBlocks.Add("% Churn", "Not calculated yet");
				
				// now get each assembly pair and compare them at the internal level...
				List<IdentifiedChangeCollection> dllChangeCollection = new List<IdentifiedChangeCollection>();
				List<IdentifiedChangeCollection> typeChangeCollection = new List<IdentifiedChangeCollection>();

				foreach (var common in summaryChanges.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies))
				{
					// description is the name of the assembly...
					var dll1 = firstVersion.Assemblies.FindMatchFor(common.Description);
					var dll2 = secondVersion.Assemblies.FindMatchFor(common.Description);

					if (dll1 != null && dll2 != null)
					{
						// inspect each assembly...

						// introspect each one...
						var reflector1 = this.ReflectorFactory.LoadAssembly(dll1.Path);
						var reflector2 = this.ReflectorFactory.LoadAssembly(dll2.Path);

						var typesIn1 = reflector1.GetTypes(AssemblyReflectionOption.Public);
						var typesIn2 = reflector2.GetTypes(AssemblyReflectionOption.Public);

						// do all comparisons...

						IdentifiedChangeCollection dllChanges = new IdentifiedChangeCollection
						{
							Name = dll1.Name,
							Heading = dll1.Name
						};

						// each inspector
						// looking for general changes to the assembly...
						foreach (var ai in this.AssemblyInspectors.Where(x => x.Enabled))
						{
							ai.Inspect(reflector1.GetAssemblyInfo(), reflector2.GetAssemblyInfo(), dllChanges);
						}

						// looking for added/removed types...
						foreach(var tci in this.TypeCollectionInspectors.Where(x => x.Enabled))
						{
							tci.Inspect(reflector1.GetTypes(AssemblyReflectionOption.Public), reflector2.GetTypes(AssemblyReflectionOption.Public), dllChanges);
						}

						// hand this off to another container...
						if (dllChanges.Changes.Any())
							dllChangeCollection.Add(dllChanges);

						// now inspect each type...
						foreach (var commonType in dllChanges.ChangesInCategory(WellKnownChangePriorities.ChangedTypes))
						{
							// find common types...
							IdentifiedChangeCollection typeChanges = new IdentifiedChangeCollection
							{
								Name = common.Description,
								Heading = dll1.Name
							};

							foreach (var ti in this.TypeInspectors.Where(x => x.Enabled))
							{
								//ti.Inspect()
							}
						}
					}
				}

				var reportRepo = new ReportingRepository();
				var reportFinder = new ReportingPluginDiscoverer(this.Finder);

				reportRepo.AddRange(reportFinder.Find());

				var writer = reportRepo.Find("html");

				if (writer != null)
				{
					IReportFormat format = writer.SupportedFormats.First();

					writer.Map = OutputFileMapBuilder.Map()
						.UsingProject(project)
						.As(format)
						.WithIndex(summaryChanges.Identifier)
						.With(summaryChanges)
						.With(dllChangeCollection)
						.With(typeChangeCollection)
						.Build();

					IReportOutput output = new FileOutput(Path.Combine(project.Settings.OutputFolder, project.Settings.IndexName));
					output.Folder = project.Settings.OutputFolder;

					writer.Write(summaryChanges, output, format);

					foreach (var dllChange in dllChangeCollection)
					{
						IReportOutput dllOutput = new FileOutput(dllChange.Name);
						output.Folder = project.Settings.OutputFolder;

						writer.Write(dllChange, output, format);
					}
				}
			}
			catch
			{
				// add to list of errors.
			}
		}

		private void RunAssemblyCollectionInspectors(IEnumerable<IAssemblyDiskInfo> first, IEnumerable<IAssemblyDiskInfo> second, IdentifiedChangeCollection changes)
		{
			foreach (var aci in this.AssemblyCollectionInspectors.Where(x => x.Enabled))
			{
				aci.Inspect(first, second, changes);
			}
		}

		private void DiscoverPlugins()
		{
			// need to be turned on or off depending on project settings...
			this.AssemblyCollectionInspectors.AddRange(new AssemblyCollectionInspectorPluginDiscoverer(this.Finder).Find());
			this.AssemblyCollectionInspectors.ForEach(x => x.Enabled = true);

			this.AssemblyInspectors.AddRange(new AssemblyInspectorPluginDiscoverer(this.Finder).Find());
			this.AssemblyInspectors.ForEach(x => x.Enabled = true);

			this.TypeCollectionInspectors.AddRange(new TypeCollectionInspectorPluginDiscoverer(this.Finder).Find());
			this.TypeCollectionInspectors.ForEach(x => x.Enabled = true);

			this.TypeInspectors.AddRange(new TypeInspectorPluginDiscoverer(this.Finder).Find());
			this.TypeInspectors.ForEach(x => x.Enabled = true);

			this.ReportWriters.AddRange(new ReportingPluginDiscoverer(this.Finder).Find());
		}
	}
}

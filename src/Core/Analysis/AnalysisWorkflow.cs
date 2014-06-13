using NDifference.Framework;
using NDifference.Inspection;
using NDifference.Inspectors;
using NDifference.Plugins;
using NDifference.Projects;
using NDifference.Reporting;
using NDifference.TypeSystem;
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
		public event EventHandler<CancellableEventArgs> AnalysisStarting;

		public event EventHandler AnalysisComplete;

		public event EventHandler<CancellableEventArgs> PluginsLoading;

		public event EventHandler PluginsComplete;

		public event EventHandler<CancellableEventArgs> AssemblyComparisonStarting;

		public event EventHandler AssemblyComparisonComplete;

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
			var cancelStart = new CancellableEventArgs();
			this.AnalysisStarting.Fire(this, cancelStart);

			if (cancelStart.CancelAction)
			{
				return;
			}

			try
			{
				this.DiscoverPlugins();

				this.AssemblyCollectionInspectors.ForEach(x => x.Enabled = !project.Settings.IgnoreInspectors.Contains(x.ShortCode));
				this.AssemblyInspectors.ForEach(x => x.Enabled = !project.Settings.IgnoreInspectors.Contains(x.ShortCode));
				this.TypeCollectionInspectors.ForEach(x => x.Enabled = !project.Settings.IgnoreInspectors.Contains(x.ShortCode));
				this.TypeInspectors.ForEach(x => x.Enabled = !project.Settings.IgnoreInspectors.Contains(x.ShortCode));

				var cancelCompare = new CancellableEventArgs();
				this.AssemblyComparisonStarting.Fire(this, cancelCompare);

				if (cancelCompare.CancelAction)
				{
					return;
				}


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
							Heading = dll1.Name,
						};

						dllChanges.SummaryBlocks.Add("Name", dll1.Name);
						dllChanges.SummaryBlocks.Add("From", reflector1.GetAssemblyInfo().Version.ToString());
						dllChanges.SummaryBlocks.Add("To", reflector2.GetAssemblyInfo().Version.ToString());
						dllChanges.SummaryBlocks.Add("% Churn", "Not calculated yet");
						
						dllChanges.CopyMetaFrom(summaryChanges);
						dllChanges.Parents.Add(new DocumentLink { Identifier = summaryChanges.Identifier, LinkText = summaryChanges.Name });

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
								Name = commonType.Description,
								Heading = dll1.Name
							};

							ITypeInfo t1 = typesIn1.FindMatchFor(commonType.Description);
							ITypeInfo t2 = typesIn2.FindMatchFor(commonType.Description);

							Debug.Assert(t1 != null, "No common type found in first assembly");
							Debug.Assert(t2 != null, "No common type found in second assembly");

							typeChanges.SummaryBlocks.Add("Name", t2.Name);
							typeChanges.SummaryBlocks.Add("Namespace", t2.Namespace);
							typeChanges.SummaryBlocks.Add("Assembly", t2.Assembly);
							typeChanges.SummaryBlocks.Add("From", reflector1.GetAssemblyInfo().Version.ToString());
							typeChanges.SummaryBlocks.Add("To", reflector2.GetAssemblyInfo().Version.ToString());
							typeChanges.SummaryBlocks.Add("% Churn", "Not calculated yet");

							typeChanges.CopyMetaFrom(summaryChanges);

							typeChanges.Parents.Add(new DocumentLink { Identifier = summaryChanges.Identifier, LinkText = summaryChanges.Name });
							typeChanges.Parents.Add(new DocumentLink { Identifier = dllChanges.Identifier, LinkText = dllChanges.Name });

							foreach (var ti in this.TypeInspectors.Where(x => x.Enabled))
							{
								ti.Inspect(t1, t2, typeChanges);
							}

							if (typeChanges.Changes.Any())
							{
								typeChangeCollection.Add(typeChanges);
							}
						}
					}
				}

				this.AssemblyComparisonComplete.Fire(this);
				
				var reportRepo = new ReportingRepository();
				var reportFinder = new ReportingPluginDiscoverer(this.Finder);

				reportRepo.AddRange(reportFinder.Find());

				var writer = reportRepo.Find("html");

				if (writer != null)
				{
					IReportFormat format = writer.SupportedFormats.First();

					writer.Map = FileMapBuilder.Map()
						.UsingProject(project)
						.As(format)
						.WithIndex(summaryChanges.Identifier)
						.With(summaryChanges)
						.With(dllChangeCollection)
						.With(typeChangeCollection)
						.Build();

					IReportOutput output = new FileOutput(Path.Combine(project.Settings.OutputFolder, project.Settings.IndexName + format.Extension));

					writer.Write(summaryChanges, output, format);

					foreach (var dllChange in dllChangeCollection)
					{
						IReportOutput dllOutput = new FileOutput(Path.Combine(project.Settings.SubPath, dllChange.Name + format.Extension));

						writer.Write(dllChange, dllOutput, format);
					}

					foreach(var typeChange in typeChangeCollection)
					{
						IReportOutput typeOutput = new FileOutput(Path.Combine(project.Settings.SubPath, typeChange.Name.HtmlSafeTypeName() + format.Extension));

						writer.Write(typeChange, typeOutput, format);
					}
				}
			}
			catch
			{
				// add to list of errors.
			}
			finally
			{
				this.AnalysisComplete.Fire(this);
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
			var cancelSearch = new CancellableEventArgs();
			this.PluginsLoading.Fire(this, cancelSearch);

			if (cancelSearch.CancelAction)
			{
				return;
			}

			// need to be turned on or off depending on project settings...
			this.AssemblyCollectionInspectors.AddRange(new AssemblyCollectionInspectorPluginDiscoverer(this.Finder).Find());

			this.AssemblyInspectors.AddRange(new AssemblyInspectorPluginDiscoverer(this.Finder).Find());

			this.TypeCollectionInspectors.AddRange(new TypeCollectionInspectorPluginDiscoverer(this.Finder).Find());

			this.TypeInspectors.AddRange(new TypeInspectorPluginDiscoverer(this.Finder).Find());

			this.ReportWriters.AddRange(new ReportingPluginDiscoverer(this.Finder).Find());

			this.PluginsComplete.Fire(this);
		}
	}
}

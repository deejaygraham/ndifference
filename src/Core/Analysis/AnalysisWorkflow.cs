using NDifference.Exceptions;
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

		public event EventHandler<CancellableEventArgs> AssemblyComparisonStarting;

		public event EventHandler AssemblyComparisonComplete;

		public event EventHandler<CancellableEventArgs> TypeComparisonStarting;

		public event EventHandler TypeComparisonComplete;

		public AnalysisWorkflow(IFileFinder finder, IAssemblyReflectorFactory reflectorFactory)
		{
			Debug.Assert(finder != null, "Finder cannot be null");

			this.Finder = finder;
			this.ReflectorFactory = reflectorFactory;
		}

		private IFileFinder Finder { get; set; }

		private IAssemblyReflectorFactory ReflectorFactory { get; set; }

		public AnalysisResult RunAnalysis(Project project, InspectorRepository inspectors)
		{
			AnalysisResult result = new AnalysisResult();

			var cancelStart = new CancellableEventArgs();
			this.AnalysisStarting.Fire(this, cancelStart);

			if (cancelStart.CancelAction)
			{
				result.Cancelled = true;
				return result;
			}
			
			try
			{
				result.Summary.Heading = project.Product.Name;
				result.Summary.Name = project.Settings.SummaryTitle;

				result.Summary.MetaBlocks.AddRange(project.Settings.GenerateMetaBlocks());
				result.Summary.FooterBlocks.AddRange(project.Settings.GenerateFooterBlocks());

				var firstVersion = project.Product.ComparedIncrements.First;
				var secondVersion = project.Product.ComparedIncrements.Second;

				RunAssemblyCollectionInspectors(inspectors, firstVersion.Assemblies, secondVersion.Assemblies, result.Summary);

				result.Summary.SummaryBlocks.Add("Name", project.Product.Name);
				result.Summary.SummaryBlocks.Add("From", firstVersion.Name);
				result.Summary.SummaryBlocks.Add("To", secondVersion.Name);
				result.Summary.SummaryBlocks.Add("% Churn", "Not calculated yet");
				
				// now get each assembly pair and compare them at the internal level...
				foreach (var common in result.Summary.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies))
				{
					var cancelCompare = new CancellableEventArgs();
					this.AssemblyComparisonStarting.Fire(this, cancelCompare);

					if (cancelCompare.CancelAction)
					{
						result.Cancelled = true;
						return result;
					}

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
							//Heading = project.Product.Name,
							Name = dll1.Name
						};

						dllChanges.SummaryBlocks.Add("Name", dll1.Name);
						dllChanges.SummaryBlocks.Add("From", reflector1.GetAssemblyInfo().Version.ToString());
						dllChanges.SummaryBlocks.Add("To", reflector2.GetAssemblyInfo().Version.ToString());
						dllChanges.SummaryBlocks.Add("% Churn", "Not calculated yet");

						dllChanges.CopyMetaFrom(result.Summary);
						dllChanges.Parents.Add(new DocumentLink { Identifier = result.Summary.Identifier, LinkText = result.Summary.Name });

						// each inspector
						// looking for general changes to the assembly...
						foreach (var ai in inspectors.AssemblyInspectors.Where(x => x.Enabled))
						{
							Trace.TraceInformation("Running assembly inspector {0}", ai.DisplayName);

							ai.Inspect(reflector1.GetAssemblyInfo(), reflector2.GetAssemblyInfo(), dllChanges);
						}

						// looking for added/removed types...
						foreach (var tci in inspectors.TypeCollectionInspectors.Where(x => x.Enabled))
						{
							Trace.TraceInformation("Running type collection inspector {0}", tci.DisplayName);

							tci.Inspect(reflector1.GetTypes(AssemblyReflectionOption.Public), reflector2.GetTypes(AssemblyReflectionOption.Public), dllChanges);
						}

						// hand this off to another container...
						if (dllChanges.Changes.Any())
							result.Assembly(dllChanges);

						// now inspect each type...
						foreach (var commonType in dllChanges.ChangesInCategory(WellKnownChangePriorities.ChangedTypes))
						{
							var cancelTypeStart = new CancellableEventArgs();
							this.TypeComparisonStarting.Fire(this, cancelTypeStart);

							if (cancelStart.CancelAction)
							{
								result.Cancelled = true;
								return result;
							}

							// find common types...
							IdentifiedChangeCollection typeChanges = new IdentifiedChangeCollection
							{
								//Heading = project.Product.Name,
								Name = commonType.Description
							};

							ITypeInfo t1 = typesIn1.FindMatchFor(commonType.Description);
							ITypeInfo t2 = typesIn2.FindMatchFor(commonType.Description);

							Debug.Assert(t1 != null, "No common type found in first assembly");
							Debug.Assert(t2 != null, "No common type found in second assembly");

							typeChanges.SummaryBlocks.Add("Name", t2.Name);
							typeChanges.SummaryBlocks.Add("Namespace", t2.Namespace);
							typeChanges.SummaryBlocks.Add("Assembly", t2.Assembly);
							typeChanges.SummaryBlocks.Add("From", reflector1.GetAssemblyInfo().Version.ToString() + " " + t1.CalculateHash());
							typeChanges.SummaryBlocks.Add("To", reflector2.GetAssemblyInfo().Version.ToString() + " " + t2.CalculateHash());
							typeChanges.SummaryBlocks.Add("% Churn", "Not calculated yet");

							typeChanges.CopyMetaFrom(result.Summary);

							typeChanges.Parents.Add(new DocumentLink { Identifier = result.Summary.Identifier, LinkText = result.Summary.Name });
							typeChanges.Parents.Add(new DocumentLink { Identifier = dllChanges.Identifier, LinkText = dllChanges.Name });

							foreach (var ti in inspectors.TypeInspectors.Where(x => x.Enabled))
							{
								Trace.TraceInformation("Running type inspector {0}", ti.DisplayName);

								ti.Inspect(t1, t2, typeChanges);
							}

							if (typeChanges.Changes.Any())
							{
								result.Type(typeChanges);
							}

							this.TypeComparisonComplete.Fire(this);
						}
					}

					this.AssemblyComparisonComplete.Fire(this);
				}
			}
			finally
			{
				this.AnalysisComplete.Fire(this);
			}

			return result;
		}

		private void RunAssemblyCollectionInspectors(InspectorRepository inspectors, IEnumerable<IAssemblyDiskInfo> first, IEnumerable<IAssemblyDiskInfo> second, IdentifiedChangeCollection changes)
		{
			foreach (var aci in inspectors.AssemblyCollectionInspectors.Where(x => x.Enabled))
			{
				Trace.TraceInformation("Running assembly collection inspector {0}", aci.DisplayName);

				aci.Inspect(first, second, changes);
			}
		}
	}
}

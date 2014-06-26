using NDifference.Framework;
using NDifference.Inspection;
using NDifference.Inspectors;
using NDifference.Projects;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Diagnostics;
using System.Linq;

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
				result.Summary.SummaryBlocks.Add("Name", project.Product.Name);

				result.Summary.MetaBlocks.AddRange(project.Settings.GenerateMetaBlocks());
				result.Summary.FooterBlocks.AddRange(project.Settings.GenerateFooterBlocks());

				var firstVersion = project.Product.ComparedIncrements.First;
				var secondVersion = project.Product.ComparedIncrements.Second;

				result.Summary.SummaryBlocks.Add("From", firstVersion.Name);
				result.Summary.SummaryBlocks.Add("To", secondVersion.Name);

				ICombinedAssemblies assemblyModel = CombinedAssemblyModel.BuildFrom(firstVersion.Assemblies, secondVersion.Assemblies);

				RunAssemblyCollectionInspectors(inspectors, assemblyModel, result.Summary);

				// now get each assembly pair and compare them at the internal level...
				foreach (var common in assemblyModel.ChangedInCommon)
				{
					var cancelCompare = new CancellableEventArgs();
					this.AssemblyComparisonStarting.Fire(this, cancelCompare);

					if (cancelCompare.CancelAction)
					{
						result.Cancelled = true;
						return result;
					}

					var dll1 = common.First;
					var dll2 = common.Second;

					// inspect each assembly...
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

					dllChanges.CopyMetaFrom(result.Summary);
					dllChanges.Parents.Add(new DocumentLink { Identifier = result.Summary.Identifier, LinkText = result.Summary.Name });

					// each inspector
					// looking for general changes to the assembly...
					foreach (var ai in inspectors.AssemblyInspectors.Where(x => x.Enabled))
					{
						ai.Inspect(reflector1.GetAssemblyInfo(), reflector2.GetAssemblyInfo(), dllChanges);
					}

					var firstTypes = reflector1.GetTypes(AssemblyReflectionOption.Public);
					var secondTypes = reflector1.GetTypes(AssemblyReflectionOption.Public);

					ICombinedTypes typeModel = CombinedObjectModel.BuildFrom(firstTypes, secondTypes);

					foreach (var tci in inspectors.TypeCollectionInspectors.Where(x => x.Enabled))
					{
						tci.Inspect(typeModel, dllChanges);
					}

					// now inspect each type...
					foreach (var commonType in typeModel.ChangedInCommon)
					{
						var cancelTypeStart = new CancellableEventArgs();
						this.TypeComparisonStarting.Fire(this, cancelTypeStart);

						if (cancelStart.CancelAction)
						{
							result.Cancelled = true;
							return result;
						}

						ITypeInfo t1 = commonType.First;
						ITypeInfo t2 = commonType.Second;

						// find common types...
						IdentifiedChangeCollection typeChanges = new IdentifiedChangeCollection
						{
							//Heading = project.Product.Name,
							Name = t1.Name
						};

						typeChanges.SummaryBlocks.Add("Name", t2.Name);
						typeChanges.SummaryBlocks.Add("Namespace", t2.Namespace);
						typeChanges.SummaryBlocks.Add("Assembly", t2.Assembly);
						typeChanges.SummaryBlocks.Add("From", reflector1.GetAssemblyInfo().Version.ToString() + " " + t1.CalculateHash());
						typeChanges.SummaryBlocks.Add("To", reflector2.GetAssemblyInfo().Version.ToString() + " " + t2.CalculateHash());

						typeChanges.CopyMetaFrom(result.Summary);

						typeChanges.Parents.Add(new DocumentLink { Identifier = result.Summary.Identifier, LinkText = result.Summary.Name });
						typeChanges.Parents.Add(new DocumentLink { Identifier = dllChanges.Identifier, LinkText = dllChanges.Name });

						foreach (var ti in inspectors.TypeInspectors.Where(x => x.Enabled))
						{
							ti.Inspect(t1, t2, typeChanges);
						}

						if (typeChanges.Changes.Any())
						{
							if (project.Settings.ConsolidateAssemblyTypes)
							{
								typeChanges.Changes.ForEach(x => dllChanges.Add(x));
							}
							else
							{
								result.Type(typeChanges);
							}
						}

						this.TypeComparisonComplete.Fire(this);
					}

					ChurnCalculator calc2 = new ChurnCalculator(typeModel);

					dllChanges.SummaryBlocks.Add("Churn", calc2.Calculate().ToString() + " %");

					this.AssemblyComparisonComplete.Fire(this);

					// hand this off to another container...
					if (dllChanges.Changes.Any())
					{
						result.Assembly(dllChanges);
					}
				}

				var overallChurn = new ChurnCalculator(assemblyModel);

				result.Summary.SummaryBlocks.Add("Churn", overallChurn.Calculate().ToString() + " %");
			}
			finally
			{
				this.AnalysisComplete.Fire(this);
			}

			return result;
		}

		private void RunAssemblyCollectionInspectors(InspectorRepository inspectors, ICombinedAssemblies assemblies, IdentifiedChangeCollection changes)
		{
			foreach (var aci in inspectors.AssemblyCollectionInspectors.Where(x => x.Enabled))
			{
				aci.Inspect(assemblies, changes);
			}
		}
	}
}

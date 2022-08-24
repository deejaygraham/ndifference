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

		public event EventHandler<FileProgessEventArgs> AnalysingAssembly;

		public event EventHandler AssemblyAnalysisComplete;

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

		public AnalysisResult RunAnalysis(Project project, InspectorRepository inspectors, IProgress<Progress> progressIndicator)
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
                // Build the main summary page, the index for the report
                result.Summary = BuilMainSummaryPage(project);

                result.BreakingChanges.CopyMetaFrom(result.Summary);
                result.BreakingChanges.Parents.Add(new DocumentLink { Identifier = result.Summary.Identifier, LinkText = result.Summary.Name });

                progressIndicator.Report(new Progress("Inspecting Release Differences"));

                var firstVersion = project.Product.ComparedIncrements.First;
                var secondVersion = project.Product.ComparedIncrements.Second;

                ICombinedAssemblies assemblyModel = CombinedAssemblyModel.BuildFrom(firstVersion.Assemblies, secondVersion.Assemblies);

                // inspect both sets of assemblies and report on differences.
                RunAssemblyCollectionInspectors(inspectors, assemblyModel, result.Summary);

                int currentAssemblyNumber = 0;
                int totalAssemblies = assemblyModel.ChangedInCommon.Count();

				// now get each assembly pair where they exist in previous and new collections
				// and compare them at the internal level...
				foreach (var commonAssemblyPair in assemblyModel.ChangedInCommon)
				{
                    ++currentAssemblyNumber;

					var cancelCompare = new CancellableEventArgs();
					this.AssemblyComparisonStarting.Fire(this, cancelCompare);

					if (cancelCompare.CancelAction)
					{
						result.Cancelled = true;
						return result;
					}

					progressIndicator.Report(new Progress("Comparing versions of " + commonAssemblyPair.First.Name));

					var previousAssembly = commonAssemblyPair.First;
					var currentAssembly = commonAssemblyPair.Second;
										
					this.AnalysingAssembly.Fire(this, new FileProgessEventArgs { FileName = currentAssembly.Name });

					// inspect each assembly...
					try
                    {
                        int breakingChangesToAssembly = 0;

                        var previousVersionReflection = this.ReflectorFactory.LoadAssembly(previousAssembly.Path);
                        var currentVersionReflection = this.ReflectorFactory.LoadAssembly(currentAssembly.Path);

                        IdentifiedChangeCollection changesToThisAssembly = new IdentifiedChangeCollection
                        {
                            Heading = previousAssembly.Name,
                            Name = previousAssembly.Name
                        };

                        //changesToThisAssembly.SummaryBlocks.Add("Name", previousAssembly.Name);
                        string previousAssemblyVersion = previousVersionReflection.GetAssemblyInfo().Version.ToString();
                        string currentAssemblyVersion = currentVersionReflection.GetAssemblyInfo().Version.ToString();

                        if (previousAssemblyVersion != currentAssemblyVersion)
                        {
                            changesToThisAssembly.SummaryBlocks.Add("From", previousAssemblyVersion);
                            changesToThisAssembly.SummaryBlocks.Add("To", currentAssemblyVersion);
                        }

                        changesToThisAssembly.CopyMetaFrom(result.Summary);
                        changesToThisAssembly.Parents.Add(new DocumentLink { Identifier = result.Summary.Identifier, LinkText = result.Summary.Name });

                        // looking for general changes to the assembly...
                        progressIndicator.Report(new Progress("Inspecting " + previousAssembly.Name, currentAssemblyNumber, totalAssemblies));

                        // run inspectors on this assembly
                        RunAssemblyInspectors(inspectors, previousVersionReflection, currentVersionReflection, changesToThisAssembly);

                        var previousTypeCollection = previousVersionReflection.GetTypes(AssemblyReflectionOption.Public);
                        var currentTypeCollection = currentVersionReflection.GetTypes(AssemblyReflectionOption.Public);

                        // filter according to namespace, other criteria - build in plugin architecture
                        // public types/members or include private, protected, internal ????

                        ICombinedTypes typeModel = CombinedObjectModel.BuildFrom(previousTypeCollection, currentTypeCollection);

                        progressIndicator.Report(new Progress("Comparing types in assembly " + previousAssembly.Name, currentAssemblyNumber, totalAssemblies));

                        // run inspectors on all the types in the assembly
                        RunTypeCollectionInspectors(inspectors, typeModel, changesToThisAssembly);

                        // now inspect each type...
                        foreach (var commonTypePair in typeModel.ChangedInCommon)
                        {
                            var cancelTypeStart = new CancellableEventArgs();
                            this.TypeComparisonStarting.Fire(this, cancelTypeStart);

                            if (cancelStart.CancelAction)
                            {
                                result.Cancelled = true;
                                return result;
                            }

                            ITypeInfo previousType = commonTypePair.First;
                            ITypeInfo currentType = commonTypePair.Second;

                            // find common types...
                            IdentifiedChangeCollection changesToThisType = new IdentifiedChangeCollection
                            {
                                //Heading = project.Product.Name,
                                Heading = previousType.Name,
                                Name = previousType.FullName
                            };

                            changesToThisType.SummaryBlocks.Add("Name", currentType.Name);
                            changesToThisType.SummaryBlocks.Add("Namespace", currentType.Namespace);
                            changesToThisType.SummaryBlocks.Add("Assembly", currentType.Assembly);

                            string fromVersion = previousVersionReflection.GetAssemblyInfo().Version.ToString();
                            string toVersion = currentVersionReflection.GetAssemblyInfo().Version.ToString();

                            bool includeTypeHash = false;

                            if (includeTypeHash)
                            {
                                fromVersion += " " + previousType.CalculateHash();
                                toVersion += " " + currentType.CalculateHash();
                            }

                            if (fromVersion == toVersion)
                            {
                                changesToThisType.SummaryBlocks.Add("Version", fromVersion);
                            }
                            else
                            {
                                changesToThisType.SummaryBlocks.Add("From", fromVersion);
                                changesToThisType.SummaryBlocks.Add("To", toVersion);
                            }

                            changesToThisType.CopyMetaFrom(result.Summary);

                            changesToThisType.Parents.Add(new DocumentLink { Identifier = result.Summary.Identifier, LinkText = result.Summary.Name });
                            changesToThisType.Parents.Add(new DocumentLink { Identifier = changesToThisAssembly.Identifier, LinkText = changesToThisAssembly.Name });

                            progressIndicator.Report(new Progress("Inspecting type " + currentType.Name));
                            RunTypeInspectors(inspectors, previousType, currentType, changesToThisType);

                            if (changesToThisType.Changes.Any())
                            {
                                if (project.Settings.ConsolidateAssemblyTypes)
                                {
                                    changesToThisType.Changes.ForEach(x => changesToThisAssembly.Add(x));
                                }
                                else
                                {
                                    result.Type(changesToThisType);

                                    var ic = new IdentifiedChange(
                                        WellKnownChangePriorities.ChangedTypes,
                                        //currentType.FullName,
                                        new DocumentLink
                                        {
                                            LinkText = currentType.FullName,
                                            LinkUrl = currentType.FullName,
                                            Identifier = currentType.Identifier
                                        });

                                    changesToThisAssembly.Add(ic);
                                }

                                int breakingChangesToType = changesToThisType.CountChangesWithSeverity(Severity.PotentiallyBreakingChange);

                                if (breakingChangesToType > 0)
                                {
                                    //foreach (var breakingChange in changesToThisType.ChangesWithSeverity(
                                    //             Severity.PotentiallyBreakingChange))
                                    //{
                                    //    // find an existing category...
                                    //    Category category =
                                    //        result.BreakingChanges.Categories.FirstOrDefault(c =>
                                    //            c.Name == breakingChange.Category.Name);

                                    //    if (category == null)
                                    //    {
                                    //        // take the category from the current list of breaking changes.
                                    //        category = new Category
                                    //        {
                                    //            Name = breakingChange.Category.Name,
                                    //            Description = breakingChange.Category.Description,
                                    //            Priority = breakingChange.Category.Priority,
                                    //            Headings = new string[] { "Changes", "Type", "Assembly" },
                                    //            Severity = breakingChange.Category.Severity
                                    //        };
                                    //    }

                                    //    var copiedChange = new IdentifiedChange
                                    //    {
                                    //        AssemblyName = breakingChange.AssemblyName,
                                    //        //Category = category,
                                    //        //Description = breakingChange.Description,
                                    //        Descriptor = breakingChange.Descriptor,
                                    //        //Inspector = breakingChange.Inspector,
                                    //        //Level = breakingChange.Level,
                                    //        Priority = breakingChange.Priority,
                                    //        TypeName = breakingChange.TypeName
                                    //    };

                                    //    // TODO: copy each one and change it
                                    //    // order by assembly and then by type name
                                    //    result.BreakingChanges.Add(copiedChange);
                                    //}

                                    //breakingChangesToAssembly += breakingChangesToType;
                                    //changesToThisType.SummaryBlocks.Add("Potential Breaking Changes", breakingChangesToType.ToString());
                                }
                            }

                            this.TypeComparisonComplete.Fire(this);
                        }

                        bool outputAssemblyChurn = false;

                        if (outputAssemblyChurn)
                        {
                            ChurnCalculator calc2 = new ChurnCalculator(typeModel);

                            changesToThisAssembly.SummaryBlocks.Add("Churn", calc2.Calculate().ToString() + " %");
                        }

                        this.AssemblyComparisonComplete.Fire(this);

                        if (changesToThisAssembly.Changes.Any())
                        {
                            result.Assembly(changesToThisAssembly);
                            result.Summary.Add(new IdentifiedChange(
                                WellKnownChangePriorities.ChangedAssemblies,
                                //commonAssemblyPair.First.Name,
                                new DocumentLink
                                {
                                    LinkText = commonAssemblyPair.First.Name,
                                    LinkUrl = commonAssemblyPair.First.Name,
                                    Identifier = commonAssemblyPair.Second.Identifier
                                }));

                            if (breakingChangesToAssembly > 0)
                                changesToThisAssembly.SummaryBlocks.Add("Potential Breaking Changes", breakingChangesToAssembly.ToString());
                        }
                    }
                    catch (BadImageFormatException)
					{
					}
					catch(Exception)
					{
						// huh ?
					}
					finally
					{
						this.AssemblyAnalysisComplete.Fire(this);
					}
				}

				bool calculateOverallChurn = false;

				if (calculateOverallChurn)
				{
					var overallChurn = new ChurnCalculator(assemblyModel);

					result.Summary.SummaryBlocks.Add("Churn", overallChurn.Calculate().ToString() + " %");
				}

                foreach(var iai in inspectors.AnalysisInspectors.Where(x => x.Enabled))
                {
                    iai.Inspect(result);
                }

                result.BreakingChanges.Name = "Potential Breaking Changes";
                result.BreakingChanges.SummaryBlocks.Add("Changes", result.BreakingChanges.Changes.Count.ToString());
                // add breaking changes to the summary list...
                // fill in missing details...

                result.Summary.Add(new IdentifiedChange(
                    WellKnownChangePriorities.ChangedAssemblies,
                   // result.BreakingChanges.Name,
                    new DocumentLink
                    {
                        LinkText = result.BreakingChanges.Name,
                        LinkUrl = result.BreakingChanges.Name,
                        Identifier = result.BreakingChanges.Identifier
                    }));
            }
            finally
			{
				this.AnalysisComplete.Fire(this);
			}

			return result;
		}

        private static void RunTypeInspectors(InspectorRepository inspectors, ITypeInfo previousType, ITypeInfo currentType, IdentifiedChangeCollection changesToThisType)
        {
            foreach (var ti in inspectors.TypeInspectors.Where(x => x.Enabled))
            {
                ti.Inspect(previousType, currentType, changesToThisType);
            }
        }

        private static IdentifiedChangeCollection BuilMainSummaryPage(Project project)
        {
            var mainSummaryPage = new IdentifiedChangeCollection
            {
                Heading = project.Settings.SummaryTitle,
                HeadingBlock = project.Settings.HeadingText,
                Name = project.Settings.SummaryTitle
            };

            mainSummaryPage.SummaryBlocks.Add("Name", project.Product.Name);

            mainSummaryPage.MetaBlocks.AddRange(project.Settings.GenerateMetaBlocks());
            mainSummaryPage.HeaderBlocks.AddRange(project.Settings.GenerateHeaderBlocks());
            mainSummaryPage.FooterBlocks.AddRange(project.Settings.GenerateFooterBlocks());

            var firstVersion = project.Product.ComparedIncrements.First;
            var secondVersion = project.Product.ComparedIncrements.Second;

            if (firstVersion.Name != secondVersion.Name)
            {
                mainSummaryPage.SummaryBlocks.Add("From", firstVersion.Name);
                mainSummaryPage.SummaryBlocks.Add("To", secondVersion.Name);
            }

            return mainSummaryPage;
        }

        private static void RunTypeCollectionInspectors(InspectorRepository inspectors, ICombinedTypes typeModel, IdentifiedChangeCollection changes)
        {
            foreach (var tci in inspectors.TypeCollectionInspectors.Where(x => x.Enabled))
            {
                tci.Inspect(typeModel, changes);
            }
        }

        private static void RunAssemblyInspectors(InspectorRepository inspectors, IAssemblyReflector previousVersionReflection, IAssemblyReflector currentVersionReflection, IdentifiedChangeCollection changes)
        {
            var prevInfo = previousVersionReflection.GetAssemblyInfo();
            var currentInfo = currentVersionReflection.GetAssemblyInfo();

            foreach (var ai in inspectors.AssemblyInspectors.Where(x => x.Enabled))
            {
                ai.Inspect(prevInfo, currentInfo, changes);
            }
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

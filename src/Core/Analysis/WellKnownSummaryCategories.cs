using NDifference.Inspection;
using System;

namespace NDifference.Analysis
{
    [Obsolete("Maybe don't need")]
	/// <summary>
	/// Changes that apply at the top level - all assemblies.
	/// </summary>
	public static class WellKnownSummaryCategories
	{
		public static readonly Category RemovedAssemblies = new Category
		{
			Name = "Removed Assemblies",
			Description = "These assemblies were removed from the new version of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.RemovedAssemblies),
			Headings = new string[] { "Assembly" },
            Severity = Severity.BreakingChange
		};

		public static readonly Category ChangedAssemblies = new Category
		{
			Name = "Changed Assemblies",
			Description = "These assemblies have changed between the two versions of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.ChangedAssemblies),
			Headings = new string[] { "Assembly" },
            Severity = Severity.PotentiallyBreakingChange
		};

        public static readonly Category PotentiallyChangedAssemblies = new Category
        {
            Name = "Potentially Changed Assemblies",
            Description = "These assemblies have failed a hash check between the two versions of the product",
            Priority = new CategoryPriority(WellKnownChangePriorities.PotentiallyChangedAssemblies),
            Headings = new string[] { "Assembly" },
            Severity = Severity.PotentiallyBreakingChange
        };

        public static readonly Category AddedAssemblies = new Category
		{
			Name = "New Assemblies",
			Description = "These assemblies have been added to the new version",
			Priority = new CategoryPriority(WellKnownChangePriorities.AddedAssemblies),
			Headings = new string[] { "Assembly" },
            Severity = Severity.NonBreaking
		};

        public static readonly Category BreakingChanges = new Category
        {
            Name = "Breaking Changes",
            Description = "These changes may break projects based on the SDK.",
            Priority = new CategoryPriority(WellKnownChangePriorities.BreakingChanges),
            Headings = new string[] { "Changes" },
            Severity = Severity.BreakingChange
        };

		//public static readonly Category UnchangedAssemblies = new Category 
		//{ 
		//	Name = "Changed Assemblies", 
		//	Description = "These assemblies have not changed between the two versions of the product",
		//	Priority = new CategoryPriority(WellKnownChangePriorities.UnchangedAssemblies) 
		//};
	}
}

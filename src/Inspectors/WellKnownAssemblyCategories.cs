using NDifference.Analysis;
using NDifference.Inspection;

namespace NDifference.Inspectors
{
	public static class WellKnownAssemblyCategories
	{
		public static readonly Category RemovedAssemblies = new Category 
		{ 
			Name = "Removed Assemblies", 
			Description = "These assemblies were removed from the new version of the product", 
			Priority = new CategoryPriority(WellKnownChangePriorities.RemovedAssemblies) 
		};

		public static readonly Category ChangedAssemblies = new Category 
		{ 
			Name = "Changed Assemblies", 
			Description = "These assemblies have changed between the two versions of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.ChangedAssemblies) 
		};

		public static readonly Category AddedAssemblies = new Category 
		{ 
			Name = "Added Assemblies", 
			Description = "These assemblies have been added to the new version",
			Priority = new CategoryPriority(WellKnownChangePriorities.AddedAssemblies) 
		};

		//public static readonly Category UnchangedAssemblies = new Category 
		//{ 
		//	Name = "Changed Assemblies", 
		//	Description = "These assemblies have not changed between the two versions of the product",
		//	Priority = new CategoryPriority(WellKnownChangePriorities.UnchangedAssemblies) 
		//};
	}
}

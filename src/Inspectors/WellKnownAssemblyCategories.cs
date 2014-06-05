using NDifference.Analysis;

namespace NDifference.Inspectors
{
	public static class WellKnownAssemblyCategories
	{
		public static readonly Category RemovedAssemblies = new Category { Name = "Removed Assemblies", Description = "These assemblies were removed from the new version of the product", Priority = new CategoryPriority(1) };

		public static readonly Category ChangedAssemblies = new Category { Name = "Changed Assemblies", Description = "These assemblies have changed between the two versions of the product", Priority = new CategoryPriority(2) };

		public static readonly Category AddedAssemblies = new Category { Name = "Added Assemblies", Description = "These assemblies have been added to the new version", Priority = new CategoryPriority(3) };
	}
}

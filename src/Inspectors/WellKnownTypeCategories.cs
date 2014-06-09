using NDifference.Analysis;
using NDifference.Inspection;

namespace NDifference.Inspectors
{
	public static class WellKnownTypeCategories
	{
		public static readonly Category AssemblyInternal = new Category
		{
			Name = "Assembly Changes",
			Description = "These changes were made to the new version of the assembly",
			Priority = new CategoryPriority(WellKnownChangePriorities.AssemblyInternal)
		};

		public static readonly Category RemovedTypes = new Category 
		{ 
			Name = "Removed Types", 
			Description = "These typers were removed from the new version of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.RemovedTypes) 
		};

		public static readonly Category ObsoleteTypes = new Category 
		{ 
			Name = "Obsolete Types", 
			Description = "These types were marked as deprecated in the new version of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.ObsoleteTypes) 
		};

		public static readonly Category ChangedTypes = new Category 
		{ 
			Name = "Changed Types", 
			Description = "These types have changed between the two versions of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.ChangedTypes) 
		};

		public static readonly Category AddedTypes = new Category 
		{ 
			Name = "Added Types", 
			Description = "These types have been added to the new version",
			Priority = new CategoryPriority(WellKnownChangePriorities.AddedTypes) 
		};

		//public static readonly Category UnchangedTypes = new Category 
		//{ 
		//	Name = "Unchanged Types", 
		//	Description = "These types have not changed between the two versions of the product",
		//	Priority = new CategoryPriority(WellKnownChangePriorities.UnchangedTypes) 
		//};
	}
}

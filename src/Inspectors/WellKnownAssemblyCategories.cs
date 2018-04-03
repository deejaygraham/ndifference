﻿using NDifference.Analysis;
using NDifference.Inspection;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Changes that apply at the assembly level
	/// </summary>
	public static class WellKnownAssemblyCategories
	{
		public static readonly Category AssemblyInternal = new Category
		{
			Name = "Assembly Changes",
			Description = "These changes were made to the new version of the assembly",
			Priority = new CategoryPriority(WellKnownChangePriorities.AssemblyInternal),
			Headings = new string[] { "Change", "From", "To" },
			CategoryType = CategoryType.Information
		};

		public static readonly Category RemovedReferences = new Category
		{
			Name = "Removed References",
			Description = "These references were removed in the new version of the assembly",
			Priority = new CategoryPriority(WellKnownChangePriorities.RemovedReferences),
			Headings = new string[] { "Reference" },
			CategoryType = CategoryType.Information
		};

		public static readonly Category AddedReferences = new Category
		{
			Name = "New References",
			Description = "These references were added to the new version of the assembly",
			Priority = new CategoryPriority(WellKnownChangePriorities.AddedReferences),
			Headings = new string[] { "Reference" },
			CategoryType = CategoryType.Information
		};

		public static readonly Category RemovedTypes = new Category
		{
			Name = "Removed Types",
			Description = "These typers were removed from the new version of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.RemovedTypes),
			Headings = new string[] { "Type" },
			CategoryType = CategoryType.Error
		};

		public static readonly Category ObsoleteTypes = new Category
		{
			Name = "Obsolete Types",
			Description = "These types were marked as deprecated in the new version of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.ObsoleteTypes),
			Headings = new string[] { "Type", "Message" },
			CategoryType = CategoryType.Warning
		};

		public static readonly Category ChangedTypes = new Category
		{
			Name = "Changed Types",
			Description = "These types have changed between the two versions of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.ChangedTypes),
			Headings = new string[] { "Type" },
			CategoryType = CategoryType.Warning
		};

		public static readonly Category AddedTypes = new Category
		{
			Name = "New Types",
			Description = "These types have been added to the new version",
			Priority = new CategoryPriority(WellKnownChangePriorities.AddedTypes),
			Headings = new string[] { "Type" },
			CategoryType = CategoryType.Information
		};

		//public static readonly Category UnchangedTypes = new Category 
		//{ 
		//	Name = "Unchanged Types", 
		//	Description = "These types have not changed between the two versions of the product",
		//	Priority = new CategoryPriority(WellKnownChangePriorities.UnchangedTypes) 
		//};
	}
}
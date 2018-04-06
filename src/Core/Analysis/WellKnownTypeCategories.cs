using NDifference.Analysis;
using NDifference.Inspection;

namespace NDifference.Inspectors
{
	public static class WellKnownTypeCategories
	{
		public static readonly Category TypeInternal = new Category
		{
			Name = "Type Changes",
			Description = "These changes were made to the new version of the type",
			Priority = new CategoryPriority(WellKnownChangePriorities.TypeInternal),
			Headings = new string[] { "Message", "Was", "Now" }
		};

		// constants
		public static readonly Category ConstantsRemoved = new Category
		{
			Name = "Removed Constants",
			Description = "These constants were removed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.ConstantsRemoved),
			Headings = new string[] { "Value" }
		};

		public static readonly Category ConstantsObsolete = new Category
		{
			Name = "Obsolete Constants",
			Description = "These constants were made obsolete in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.ConstantsObsolete),
			Headings = new string[] { "Value", "Message" }
		};

		public static readonly Category ConstantsChanged = new Category
		{
			Name = "Changed Constants",
			Description = "These constants were changed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.ConstantsChanged),
			Headings = new string[] { "Message", "Was", "Now" }
		};

		public static readonly Category ConstantsAdded = new Category
		{
			Name = "New Constants",
			Description = "These constants were added in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.ConstantsAdded),
			Headings = new string[] { "Value" }
		};

		// fields
		public static readonly Category FieldsRemoved = new Category
		{
			Name = "Removed Fields",
			Description = "These fields were removed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.FieldsRemoved),
			Headings = new string[] { "Value" }
		};

		public static readonly Category FieldsObsolete = new Category
		{
			Name = "Obsolete Fields",
			Description = "These fields were made obsolete in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.FieldsObsolete),
			Headings = new string[] { "Value", "Message" }
		};

		public static readonly Category FieldsChanged = new Category
		{
			Name = "Changed Fields",
			Description = "These fields were changed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.FieldsChanged),
			Headings = new string[] { "Message", "Was", "Now" }
		};

		public static readonly Category FieldsAdded = new Category
		{
			Name = "New Fields",
			Description = "These fields were added in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.FieldsAdded),
			Headings = new string[] { "Value" }
		};

		// constructors
		public static readonly Category ConstructorsRemoved = new Category
		{
			Name = "Removed Constructors",
			Description = "These constructors were removed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.ConstructorsRemoved),
			Headings = new string[] { "Signature" }
		};

		public static readonly Category ConstructorsObsolete = new Category
		{
			Name = "Obsolete Constructors",
			Description = "These constructors were made obsolete in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.ConstructorsObsolete),
			Headings = new string[] { "Signature", "Message" }
		};

		public static readonly Category ConstructorsChanged = new Category
		{
			Name = "Changed Constructors",
			Description = "These constructors were changed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.ConstructorsChanged),
			Headings = new string[] { "Was", "Now" }
		};

		public static readonly Category ConstructorsAdded = new Category
		{
			Name = "New Constructors",
			Description = "These constructors were added in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.ConstructorsAdded),
			Headings = new string[] { "Signature" }
		};

		// finalizers
		public static readonly Category FinalizersRemoved = new Category
		{
			Name = "Removed Finalizer",
			Description = "This finalizer was removed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.FinalizersRemoved),
			Headings = new string[] { "Signature" }
		};

		public static readonly Category FinalizersObsolete = new Category
		{
			Name = "Obsolete Finalizer",
			Description = "This finalizer was made obsolete in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.FinalizersObsolete),
			Headings = new string[] { "Signature", "Message" }
		};

		public static readonly Category FinalizersChanged = new Category
		{
			Name = "Changed Finalizer",
			Description = "This finalizer was changed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.FinalizersChanged),
			Headings = new string[] { "Was", "Now" }
		};

		public static readonly Category FinalizersAdded = new Category
		{
			Name = "New Finalizer",
			Description = "This finalizer was added in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.FinalizersAdded),
			Headings = new string[] { "Signature" }
		};

		// delegates
		public static readonly Category DelegatesRemoved = new Category
		{
			Name = "Removed Delegates",
			Description = "These delegates were removed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.DelegatesRemoved),
			Headings = new string[] { "Signature" }
		};

		public static readonly Category DelegatesObsolete = new Category
		{
			Name = "Obsolete Delegates",
			Description = "These delegates were made obsolete in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.DelegatesObsolete),
			Headings = new string[] { "Signature", "Message" }
		};

		public static readonly Category DelegatesChanged = new Category
		{
			Name = "Changed Delegates",
			Description = "These delegates were changed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.DelegatesChanged),
			Headings = new string[] { "Was", "Now" }
		};

		public static readonly Category DelegatesAdded = new Category
		{
			Name = "New Delegates",
			Description = "These delegates were added in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.DelegatesAdded),
			Headings = new string[] { "Signature" }
		};

		// events
		public static readonly Category EventsRemoved = new Category
		{
			Name = "Removed Events",
			Description = "These events were removed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.EventsRemoved),
			Headings = new string[] { "Signature" }
		};

		public static readonly Category EventsObsolete = new Category
		{
			Name = "Obsolete Events",
			Description = "These events were made obsolete in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.EventsObsolete),
			Headings = new string[] { "Signature", "Message" }
		};

		public static readonly Category EventsChanged = new Category
		{
			Name = "Changed Events",
			Description = "These events were changed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.EventsChanged),
			Headings = new string[] { "Message", "Was", "Now" }
		};

		public static readonly Category EventsAdded = new Category
		{
			Name = "New Events",
			Description = "These events were added in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.EventsAdded),
			Headings = new string[] { "Signature" }
		};

		// properties
		public static readonly Category PropertiesRemoved = new Category
		{
			Name = "Removed Properties",
			Description = "These properties were removed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.PropertiesRemoved),
			Headings = new string[] { "Signature" }
		};

		public static readonly Category PropertiesObsolete = new Category
		{
			Name = "Obsolete Properties",
			Description = "These properties were made obsolete in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.PropertiesObsolete),
			Headings = new string[] { "Signature", "Message" }
		};

		public static readonly Category PropertiesChanged = new Category
		{
			Name = "Changed Properties",
			Description = "These properties were changed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.PropertiesChanged),
			Headings = new string[] { "Message", "Was", "Now" }
		};

		public static readonly Category PropertiesAdded = new Category
		{
			Name = "New Properties",
			Description = "These properties were added in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.PropertiesAdded),
			Headings = new string[] { "Signature" }
		};

		// indexers
		public static readonly Category IndexersRemoved = new Category
		{
			Name = "Removed Indexers",
			Description = "These indexers were removed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.IndexersRemoved),
			Headings = new string[] { "Signature" }
		};

		public static readonly Category IndexersObsolete = new Category
		{
			Name = "Obsolete Indexers",
			Description = "These indexers were made obsolete in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.IndexersObsolete),
			Headings = new string[] { "Signature", "Message" }
		};

		public static readonly Category IndexersChanged = new Category
		{
			Name = "Changed Indexers",
			Description = "These indexers were changed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.IndexersChanged),
			Headings = new string[] { "Was", "Now" }
		};

		public static readonly Category IndexersAdded = new Category
		{
			Name = "New Indexers",
			Description = "These indexers were added in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.IndexersAdded),
			Headings = new string[] { "Signature" }
		};

		// methods
		public static readonly Category MethodsRemoved = new Category
		{
			Name = "Removed Methods",
			Description = "These methods were removed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.MethodsRemoved),
			Headings = new string[] { "Signature" }
		};

		public static readonly Category MethodsObsolete = new Category
		{
			Name = "Obsolete Methods",
			Description = "These methods were made obsolete in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.MethodsObsolete),
			Headings = new string[] { "Signature", "Message" }
		};

		public static readonly Category MethodsChanged = new Category
		{
			Name = "Changed Methods",
			Description = "These methods were changed in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.MethodsChanged),
			Headings = new string[] { "Message", "Was", "Now" }
		};

		public static readonly Category MethodsAdded = new Category
		{
			Name = "New Methods",
			Description = "These methods were added in the new version of this type",
			Priority = new CategoryPriority(WellKnownChangePriorities.MethodsAdded),
			Headings = new string[] { "Signature" }
		};

		// enums
		public static readonly Category EnumValuesRemoved = new Category
		{
			Name = "Values Removed",
			Description = "These values were removed in the new version of this enum",
			Priority = new CategoryPriority(WellKnownChangePriorities.EnumValuesRemoved),
			Headings = new string[] { "Value" }
		};

		public static readonly Category EnumValuesChanged = new Category
		{
			Name = "Values Changed",
			Description = "These values were changed in the new version of this enum",
			Priority = new CategoryPriority(WellKnownChangePriorities.EnumValuesChanged),
			Headings = new string[] { "Was", "Now" }
		};

		public static readonly Category EnumValuesAdded = new Category
		{
			Name = "Values Added",
			Description = "These values were added to the new version of this enum",
			Priority = new CategoryPriority(WellKnownChangePriorities.EnumValuesAdded),
			Headings = new string[] { "Value" }
		};



		public static readonly Category TypeDebug = new Category
		{
			Name = "Debug",
			Description = "These are debug output that should not appear in the finished reports.",
			Priority = new CategoryPriority(WellKnownChangePriorities.TypeDebug)
		};

	}
}

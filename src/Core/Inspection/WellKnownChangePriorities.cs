
namespace NDifference.Inspection
{
	/// <summary>
	/// Priority order for changes in reports.
	/// </summary>
	public static class WellKnownChangePriorities
	{
		// summary level
		public static readonly int SummaryInternal = 0;

		public static readonly int RemovedAssemblies = SummaryInternal + 1;

        public static readonly int BreakingChanges = RemovedAssemblies + 1;

		public static readonly int ChangedAssemblies = BreakingChanges + 1;

        public static readonly int PotentiallyChangedAssemblies = ChangedAssemblies + 1;

        public static readonly int AddedAssemblies = PotentiallyChangedAssemblies + 1;

		//public static readonly int UnchangedAssemblies = 4;

		// assembly level
		public static readonly int AssemblyInternal = 10;

		public static readonly int RemovedReferences = AssemblyInternal + 1;

		public static readonly int AddedReferences = RemovedReferences + 1;

		public static readonly int RemovedTypes = AddedReferences + 1;

		public static readonly int ObsoleteTypes = RemovedTypes + 1;

        public static readonly int ChangedTypes = ObsoleteTypes + 1;

        public static readonly int PotentiallyChangedTypes = ChangedTypes + 1;

		public static readonly int AddedTypes = PotentiallyChangedTypes + 1;

		// public static readonly int UnchangedTypes = 5;

		// type level
		public static readonly int TypeInternal = 100;

		// constants
		public static readonly int ConstantsRemoved = TypeInternal + 1;

		public static readonly int ConstantsObsolete = ConstantsRemoved + 1;

		public static readonly int ConstantsChanged = ConstantsObsolete + 1;

		public static readonly int ConstantsAdded = ConstantsChanged + 1;
		
		// fields
		public static readonly int FieldsRemoved = ConstantsAdded + 1;

		public static readonly int FieldsObsolete = FieldsRemoved + 1;

		public static readonly int FieldsChanged = FieldsObsolete + 1;

		public static readonly int FieldsAdded = FieldsChanged + 1;

		// constructors
		public static readonly int ConstructorsRemoved = FieldsAdded + 1;

		public static readonly int ConstructorsObsolete = ConstructorsRemoved + 1;

		public static readonly int ConstructorsChanged = ConstructorsObsolete + 1;

		public static readonly int ConstructorsAdded = ConstructorsChanged + 1;

		// static constructors

		// finalizers
		public static readonly int FinalizersRemoved = ConstructorsAdded + 1;

		public static readonly int FinalizersObsolete = FinalizersRemoved + 1;

		public static readonly int FinalizersChanged = FinalizersObsolete + 1;

		public static readonly int FinalizersAdded = FinalizersChanged + 1;

		// delegates
		public static readonly int DelegatesRemoved = FinalizersAdded + 1;

		public static readonly int DelegatesObsolete = DelegatesRemoved + 1;

		public static readonly int DelegatesChanged = DelegatesObsolete + 1;

		public static readonly int DelegatesAdded = DelegatesChanged + 1;
		
		// events
		public static readonly int EventsRemoved = DelegatesAdded + 1;

		public static readonly int EventsObsolete = EventsRemoved + 1;

		public static readonly int EventsChanged = EventsObsolete + 1;

		public static readonly int EventsAdded = EventsChanged + 1;

		// properties
		public static readonly int PropertiesRemoved = EventsAdded + 1;

		public static readonly int PropertiesObsolete = PropertiesRemoved + 1;

		public static readonly int PropertiesChanged = PropertiesObsolete + 1;

		public static readonly int PropertiesAdded = PropertiesChanged + 1;

		// indexers
		public static readonly int IndexersRemoved = PropertiesAdded + 1;

		public static readonly int IndexersObsolete = IndexersRemoved + 1;

		public static readonly int IndexersChanged = IndexersObsolete + 1;

		public static readonly int IndexersAdded = IndexersChanged + 1;

		// methods
		public static readonly int MethodsRemoved = IndexersAdded + 1;

		public static readonly int MethodsObsolete = MethodsRemoved + 1;

		public static readonly int MethodsChanged = MethodsObsolete + 1;

		public static readonly int MethodsAdded = MethodsChanged + 1;

		// enums
		public static readonly int EnumValuesRemoved = MethodsAdded + 1;

		public static readonly int EnumValuesChanged = EnumValuesRemoved + 1;

		public static readonly int EnumValuesAdded = EnumValuesChanged + 1;

		// debugging
		public static readonly int TypeDebug = 999;

	}
}

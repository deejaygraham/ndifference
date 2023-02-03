
using System.Collections.Generic;
using NDifference.Analysis;

namespace NDifference.Inspection
{
	/// <summary>
	/// Order of changes reported to user.
	/// </summary>
	public static class WellKnownChangePriorities
	{
		// summary level
		public static readonly int SummaryInternal = 0;

		public static readonly int RemovedAssemblies = SummaryInternal + 10;

        public static readonly int BreakingChanges = RemovedAssemblies + 10;

		public static readonly int ChangedAssemblies = BreakingChanges + 10;

        public static readonly int PotentiallyChangedAssemblies = ChangedAssemblies + 10;

        public static readonly int AddedAssemblies = PotentiallyChangedAssemblies + 10;

		//public static readonly int UnchangedAssemblies = 4;

		// assembly level
		public static readonly int AssemblyInternal = 100;

		public static readonly int RemovedReferences = AssemblyInternal + 10;

		public static readonly int AddedReferences = RemovedReferences + 10;

		public static readonly int RemovedTypes = AddedReferences + 10;

		public static readonly int ObsoleteTypes = RemovedTypes + 10;

        public static readonly int ChangedTypes = ObsoleteTypes + 10;

        public static readonly int PotentiallyChangedTypes = ChangedTypes + 10;

		public static readonly int AddedTypes = PotentiallyChangedTypes + 10;

		// public static readonly int UnchangedTypes = 5;

		// type level
		public static readonly int TypeInternal = 500;

		// constants
		public static readonly int ConstantsRemoved = TypeInternal + 10;

		public static readonly int ConstantsObsolete = ConstantsRemoved + 10;

		public static readonly int ConstantsChanged = ConstantsObsolete + 10;

		public static readonly int ConstantsAdded = ConstantsChanged + 10;
		
		// fields
		public static readonly int FieldsRemoved = ConstantsAdded + 10;

		public static readonly int FieldsObsolete = FieldsRemoved + 10;

		public static readonly int FieldsChanged = FieldsObsolete + 10;

		public static readonly int FieldsAdded = FieldsChanged + 10;

		// constructors
		public static readonly int ConstructorsRemoved = FieldsAdded + 10;

		public static readonly int ConstructorsObsolete = ConstructorsRemoved + 10;

		public static readonly int ConstructorsChanged = ConstructorsObsolete + 10;

		public static readonly int ConstructorsAdded = ConstructorsChanged + 10;

		// static constructors

		// finalizers
		public static readonly int FinalizersRemoved = ConstructorsAdded + 10;

		public static readonly int FinalizersObsolete = FinalizersRemoved + 10;

		public static readonly int FinalizersChanged = FinalizersObsolete + 10;

		public static readonly int FinalizersAdded = FinalizersChanged + 10;

		// delegates
		public static readonly int DelegatesRemoved = FinalizersAdded + 10;

		public static readonly int DelegatesObsolete = DelegatesRemoved + 10;

		public static readonly int DelegatesChanged = DelegatesObsolete + 10;

		public static readonly int DelegatesAdded = DelegatesChanged + 10;
		
		// events
		public static readonly int EventsRemoved = DelegatesAdded + 10;

		public static readonly int EventsObsolete = EventsRemoved + 10;

		public static readonly int EventsChanged = EventsObsolete + 10;

		public static readonly int EventsAdded = EventsChanged + 10;

		// properties
		public static readonly int PropertiesRemoved = EventsAdded + 10;

		public static readonly int PropertiesObsolete = PropertiesRemoved + 10;

		public static readonly int PropertiesChanged = PropertiesObsolete + 10;

		public static readonly int PropertiesAdded = PropertiesChanged + 10;

		// indexers
		public static readonly int IndexersRemoved = PropertiesAdded + 10;

		public static readonly int IndexersObsolete = IndexersRemoved + 10;

		public static readonly int IndexersChanged = IndexersObsolete + 10;

		public static readonly int IndexersAdded = IndexersChanged + 10;

		// methods
		public static readonly int MethodsRemoved = IndexersAdded + 10;

		public static readonly int MethodsObsolete = MethodsRemoved + 10;

		public static readonly int MethodsChanged = MethodsObsolete + 10;

		public static readonly int MethodsAdded = MethodsChanged + 10;

		// enums
		public static readonly int EnumValuesRemoved = MethodsAdded + 10;

		public static readonly int EnumValuesChanged = EnumValuesRemoved + 10;

		public static readonly int EnumValuesAdded = EnumValuesChanged + 10;

		// debugging
		public static readonly int TypeDebug = 999;

        public static IEnumerable<string> ColumnNames(int change, bool includeTypeAndAssembly = false)
        {
            var columns = new List<string>();

            if (change == WellKnownChangePriorities.RemovedAssemblies ||
                change == WellKnownChangePriorities.AddedAssemblies ||
                change == WellKnownChangePriorities.ChangedAssemblies)
            {
                // single column
                columns.Add("Assembly");
            }
            else if (change == WellKnownChangePriorities.BreakingChanges ||
                     change == WellKnownChangePriorities.PotentiallyChangedAssemblies) // not used
            {
                columns.Add("Breaking Changes");
            }
            else if (change == WellKnownChangePriorities.AssemblyInternal ||
                     change == WellKnownChangePriorities.TypeInternal)
            {
                // class structure has changed
                columns.Add("Was");
                columns.Add("Is Now");
                columns.Add("Reason");
            }
            else if (change == WellKnownChangePriorities.AddedReferences || 
                     change == WellKnownChangePriorities.RemovedReferences)
            {
                columns.Add("Reference");
            }
            else if (change == WellKnownChangePriorities.AddedTypes ||
                     change == WellKnownChangePriorities.RemovedTypes ||
                     change == WellKnownChangePriorities.ChangedTypes)
            {
                columns.Add("Type");
            }
            else if (change == WellKnownChangePriorities.ObsoleteTypes)
            {
                columns.Add("Type");
                columns.Add("Reason");
            }
            else if (change == WellKnownChangePriorities.PotentiallyChangedTypes) // not used
            {
                columns.Add("Potentially Changed Types");
            }
            else if (change == WellKnownChangePriorities.ConstantsAdded || 
                     change == WellKnownChangePriorities.ConstantsRemoved ||
                     change == WellKnownChangePriorities.ConstructorsAdded ||
                     change == WellKnownChangePriorities.ConstructorsRemoved ||
                     change == WellKnownChangePriorities.DelegatesAdded ||
                     change == WellKnownChangePriorities.DelegatesRemoved ||
                     change == WellKnownChangePriorities.EnumValuesAdded ||
                     change == WellKnownChangePriorities.EnumValuesRemoved ||
                     change == WellKnownChangePriorities.EventsAdded ||
                     change == WellKnownChangePriorities.EventsRemoved ||
                     change == WellKnownChangePriorities.FinalizersAdded ||
                     change == WellKnownChangePriorities.FinalizersRemoved ||
                     change == WellKnownChangePriorities.FieldsAdded ||
                     change == WellKnownChangePriorities.FieldsRemoved ||
                     change == WellKnownChangePriorities.IndexersAdded ||
                     change == WellKnownChangePriorities.IndexersRemoved ||
                     change == WellKnownChangePriorities.MethodsAdded ||
                     change == WellKnownChangePriorities.MethodsRemoved ||
                     change == WellKnownChangePriorities.PropertiesAdded ||
                     change == WellKnownChangePriorities.PropertiesRemoved)
            {
                columns.Add("Signature");
            }
            else if (change == WellKnownChangePriorities.ConstantsObsolete ||
                     change == WellKnownChangePriorities.ConstructorsObsolete ||
                     change == WellKnownChangePriorities.DelegatesObsolete ||
                     change == WellKnownChangePriorities.EventsObsolete ||
                     change == WellKnownChangePriorities.FieldsObsolete ||
                     change == WellKnownChangePriorities.FinalizersObsolete ||
                     //change == WellKnownChangePriorities.IndexersObsolete ||
                     change == WellKnownChangePriorities.MethodsObsolete ||
                     change == WellKnownChangePriorities.PropertiesObsolete)
            {
                columns.Add("Signature");
                columns.Add("Reason");
            }
            else if (change == WellKnownChangePriorities.ConstantsChanged ||
                     change == WellKnownChangePriorities.ConstructorsChanged ||
                     change == WellKnownChangePriorities.DelegatesChanged ||
                     change == WellKnownChangePriorities.EnumValuesChanged ||
                     change == WellKnownChangePriorities.EventsChanged ||
                     change == WellKnownChangePriorities.FieldsChanged ||
                     //change == WellKnownChangePriorities.FinalizersChanged ||
                     //change == WellKnownChangePriorities.IndexersChanged ||
                     change == WellKnownChangePriorities.MethodsChanged ||
                     change == WellKnownChangePriorities.PropertiesChanged)
            {
                columns.Add("Was");
                columns.Add("Is Now");
                columns.Add("Reason");
            }
            else if (change == WellKnownChangePriorities.TypeDebug)
            {
                // nothing
                columns.Add("Debug");
            }
            else
            {
                columns.Add("Priority Not Handled!");
            }

            if (includeTypeAndAssembly)
            {
                columns.Add("Type");
                columns.Add("Assembly");
            }

            return columns;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspection
{
	/// <summary>
	/// Priority order for changes in reports.
	/// </summary>
	public static class WellKnownChangePriorities
	{
		// summary level
		public static readonly int SummaryInternal = 0;

		public static readonly int RemovedAssemblies = 1;

		public static readonly int ChangedAssemblies = 2;

		public static readonly int AddedAssemblies = 3;

		//public static readonly int UnchangedAssemblies = 4;

		// assembly level
		public static readonly int AssemblyInternal = 0;

		public static readonly int RemovedTypes = 1;

		public static readonly int ObsoleteTypes = 2;

		public static readonly int ChangedTypes = 3;

		public static readonly int AddedTypes = 4;

		// public static readonly int UnchangedTypes = 5;

		// type level
		public static readonly int TypeInternal = 0;

		// constants
		public static readonly int ConstantsRemoved = 1;

		public static readonly int ConstantsObsolete = 2;

		public static readonly int ConstantsChanged = 3;

		public static readonly int ConstantsAdded = 4;
		
		// fields
		public static readonly int FieldsRemoved = 5;

		public static readonly int FieldsObsolete = 6;

		public static readonly int FieldsChanged = 7;

		public static readonly int FieldsAdded = 8;

		// constructors
		public static readonly int ConstructorsRemoved = 9;

		public static readonly int ConstructorsObsolete = 10;

		public static readonly int ConstructorsChanged = 11;

		public static readonly int ConstructorsAdded = 12;

		// static constructors

		// finalizers
		public static readonly int FinalizersRemoved = 13;

		public static readonly int FinalizersObsolete = 14;

		public static readonly int FinalizersChanged = 15;

		public static readonly int FinalizersAdded = 16;

		// delegates
		public static readonly int DelegatesRemoved = 17;

		public static readonly int DelegatesObsolete = 18;

		public static readonly int DelegatesChanged = 19;

		public static readonly int DelegatesAdded = 20;
		
		// events
		public static readonly int EventsRemoved = 21;

		public static readonly int EventsObsolete = 22;

		public static readonly int EventsChanged = 23;

		public static readonly int EventsAdded = 24;

		// properties
		public static readonly int PropertiesRemoved = 25;

		public static readonly int PropertiesObsolete = 26;

		public static readonly int PropertiesChanged = 27;

		public static readonly int PropertiesAdded = 28;

		// indexers
		public static readonly int IndexersRemoved = 29;

		public static readonly int IndexersObsolete = 30;

		public static readonly int IndexersChanged = 31;
	
		public static readonly int IndexersAdded = 32;

		// methods
		public static readonly int MethodsRemoved = 33;

		public static readonly int MethodsObsolete = 34;

		public static readonly int MethodsChanged = 35;

		public static readonly int MethodsAdded = 36;

		// enums


		public static readonly int TypeDebug = 999;

	}
}

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

	}
}

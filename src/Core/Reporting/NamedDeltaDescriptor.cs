using System;
using System.Collections.Generic;

namespace NDifference.Reporting
{
	/// <summary>
	/// Describes a change that includes a name, previous value and current value.
	/// </summary>
	[Obsolete("Use DeltaDescriptor with Description field")]
	public class NamedDeltaDescriptor : INamedDeltaDescriptor
	{
		public string Name { get; set; }

		public string Reason { get; set; }

		public string Was { get; set; }

		public string IsNow { get; set; }

		public int Columns { get { return 3; } }

		public IEnumerable<string> ColumnNames
		{
			get
			{
				return new List<string>();
			}
		}
	}
}

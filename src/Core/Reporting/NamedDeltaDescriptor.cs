using System.Collections.Generic;

namespace NDifference.Reporting
{
	/// <summary>
	/// Describes a change that includes a name, previous value and current value.
	/// </summary>
	public class NamedDeltaDescriptor : INamedDeltaDescriptor
	{
		public string Name { get; set; }

		public object Was { get; set; }

		public object IsNow { get; set; }

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

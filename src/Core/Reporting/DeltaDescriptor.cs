using System.Collections.Generic;

namespace NDifference.Reporting
{
    /// <summary>
    /// Describes a change from a "was" state to an 
    /// "is now" state. With optional descriptive text.
    /// </summary>
    public class DeltaDescriptor : IDeltaDescriptor
	{
		// public string Name { get; set; }

		// TODO make this string
		public string Was { get; set; }

		// TODO make this string
		public string IsNow { get; set; }

		public string Reason { get; set; }

		public int Columns { get { return 2; } }

		public IEnumerable<string> ColumnNames 
		{ 
			get
            {
				return new List<string>();
            } 
		}
	}
}

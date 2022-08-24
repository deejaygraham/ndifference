using NDifference.SourceFormatting;
using System.Collections.Generic;

namespace NDifference.Reporting
{
	/// <summary>
	/// Describes a code level change from a "was" state to an 
	/// "is now" state. With optional descriptive text.
	/// </summary>
	public class CodeDeltaDescriptor : ICodeDeltaDescriptor
	{
		// public string Name { get; set; }

		// TODO make this string
		public ICoded Was { get; set; }

		// TODO make this string
		public ICoded IsNow { get; set; }

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

using System.Collections.Generic;

namespace NDifference.Reporting
{
	/// <summary>
	/// Generic descriptor used to provide extra detail for
	/// difference reports.
	/// </summary>
	public interface IDescriptor
	{
		/// <summary>
		/// Why the difference?
		/// </summary>
		string Reason { get; }

		// TODO move these to report only???
		int Columns { get; }

		IEnumerable<string> ColumnNames { get; }
	
	}
}

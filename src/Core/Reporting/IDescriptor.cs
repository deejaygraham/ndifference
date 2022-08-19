using System.Collections.Generic;

namespace NDifference.Reporting
{
	/// <summary>
	/// Generic descriptor used to provide extra detail for
	/// difference reports.
	/// </summary>
	public interface IDescriptor
	{
		int Columns { get; }

		IEnumerable<string> ColumnNames { get; }
	
	}
}

using System.Collections.Generic;
using System.Linq;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Filters or disables individual inspectors matching a list of
	/// those to ignore.
	/// </summary>
	public class InspectorFilter
	{
		private List<string> shortCodes;

		/// <summary>
		/// Create a filter using a delimited list of codes matching those
		/// to ignore or disable.
		/// </summary>
		/// <param name="delimitedList">semi-colon delimited list of short codes</param>
		public InspectorFilter(string delimitedList)
		{
			this.shortCodes = new List<string>(delimitedList.Split(';'));
		}

		/// <summary>
		/// Disables any inspector whose short code matches any in its list.
		/// </summary>
		/// <param name="inspectors">Current list of inspectors.</param>
		public void Filter(IEnumerable<IInspector> inspectors)
		{
			foreach (var i in inspectors.Where( x => this.shortCodes.Contains(x.ShortCode)))
			{
				i.Enabled = false;
			}
		}
	}
}

using System.Collections.Generic;

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
		/// Enables or disables inspectors according to the current ignore list.
		/// </summary>
		/// <param name="inspectors">Current list of inspectors.</param>
		public void Filter(IEnumerable<IInspector> inspectors)
		{
			foreach (var i in inspectors)
			{
				i.Enabled = !this.shortCodes.Contains(i.ShortCode);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class InspectorFilter
	{
		private List<string> shortCodes;

		public InspectorFilter(string delimitedList)
		{
			this.shortCodes = new List<string>(delimitedList.Split(';'));
		}

		public void Filter(IEnumerable<IInspector> inspectors)
		{
			foreach (var i in inspectors)
			{
				if (this.shortCodes.Contains(i.ShortCode))
				{
					i.Enabled = false;
				}
			}
		}
	}
}

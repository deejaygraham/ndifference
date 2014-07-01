using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Framework
{
	public class FileProgessEventArgs : EventArgs
	{
		public string FileName { get; set; }

		public int Item { get; set; }

		public int Total { get; set; }
	}
}

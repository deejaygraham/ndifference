using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Framework
{
	public class CancellableEventArgs : EventArgs
	{
		public bool CancelAction { get; set; }
	}
}

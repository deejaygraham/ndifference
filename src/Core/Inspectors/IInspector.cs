using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public interface IInspector
	{
		bool Enabled { get; set; }

		string ShortCode { get; }

		string DisplayName { get; }

		string Description { get; }
	}
}

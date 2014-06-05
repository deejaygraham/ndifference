using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Analysis
{
	/// <summary>
	/// Represents a single change in an API.
	/// </summary>
	public sealed class IdentifiedChange
	{
		public IdentifiedChange()
		{
			this.Priority = CategoryPriority.InvalidValue;
		}

		public string Description { get; set; }

		public CategoryPriority Priority { get; set; }

		public object Descriptor { get; set; }

		// public ISourceCode Code { get; set; }

		// public ISourceDelta Delta { get; set; }

		// descriptor object...
	}

}

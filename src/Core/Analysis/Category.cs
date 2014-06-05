using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Analysis
{
	[DebuggerDisplay("{Name}")]
	public class Category : IUniquelyIdentifiable
	{
		public Category()
		{
			this.Identifier = new Identifier().Value;
			this.Priority = CategoryPriority.InvalidValue;
		}

		public string Identifier { get; private set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public CategoryPriority Priority { get; set; }
	}
}

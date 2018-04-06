using System;
using System.Diagnostics;

namespace NDifference.Analysis
{
	[DebuggerDisplay("{Name}")]
	public class Category : IUniquelyIdentifiable
	{
		public Category()
		{
			this.Identifier = new Identifier().Value;
			this.Priority = new CategoryPriority(CategoryPriority.InvalidValue);
		}

		public string Identifier { get; private set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public CategoryPriority Priority { get; set; }

		public string[] Headings { get; set; }

		public Severity Severity { get; set; }

		public int Columns { get { return this.Headings.Length; } }
	}

	
}

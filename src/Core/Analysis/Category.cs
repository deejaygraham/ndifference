using System;
using System.Diagnostics;
using System.Text;

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

        public string FullDescription
        {
            get
            {
                if (this.Severity <= Severity.NonBreaking)
                    return this.Description;

                var builder = new StringBuilder(this.Description);

                if (!this.Description.EndsWith("."))
                    builder.Append(". ");

                builder.Append("Please check client code to assess likely impact.");

                return builder.ToString();
            }
        }

		public CategoryPriority Priority { get; set; }

		public string[] Headings { get; set; }

		public Severity Severity { get; set; }

		public int Columns { get { return this.Headings.Length; } }
	}

	
}

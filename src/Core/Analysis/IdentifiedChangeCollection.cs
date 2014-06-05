using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Analysis
{
	public sealed class IdentifiedChangeCollection : IUniquelyIdentifiable
	{
		public IdentifiedChangeCollection()
		{
			this.Identifier = new Identifier().ToString();

			this.MetaBlocks = new List<string>();
			this.FooterBlocks = new List<string>();
			this.SummaryBlocks = new Dictionary<string, string>();

			this.Categories = new HashSet<Category>();
			this.Changes = new List<IdentifiedChange>();
		}

		public string Identifier { get; set; }

		public string Name { get; set; }

		// Review - are these necessary? 
		// may need to be on another object filled in immediately before
		// generating reports.
		public List<string> MetaBlocks { get; private set; }

		public string Heading { get; set; }

		public string HeadingBlock { get; set; }

		public Dictionary<string, string> SummaryBlocks { get; private set; }

		public List<string> FooterBlocks { get; private set; }

		public HashSet<Category> Categories { get; private set; }

		public List<IdentifiedChange> Changes { get; private set; }

		public void Add(Category cat)
		{
			Debug.Assert(this.Categories != null, "Categories collection is null");
			Debug.Assert(cat != null, "Category is null");
			Debug.Assert(!String.IsNullOrEmpty(cat.Name), "Category must be named");
			Debug.Assert(cat.Priority.IsValid, "Category order is not set");

			this.Categories.Add(cat);
		}

		public void Add(IdentifiedChange change)
		{
			Debug.Assert(this.Changes != null, "Changes collection is null");
			Debug.Assert(change != null, "Change is null");
			this.Changes.Add(change);
		}

		public List<IdentifiedChange> ChangesInCategory(CategoryPriority priority)
		{
			Debug.Assert(this.Categories != null, "Categories collection is null");
			Debug.Assert(this.Changes != null, "Changes collection is null");
			Debug.Assert(priority .IsValid, "Invalid order value");
			//Debug.Assert(this.Categories.Any(c => c.Priority == priority), "Invalid order value");

			return this.Changes.Where(x => x.Priority == priority.Value).ToList();
		}

		public List<IdentifiedChange> UnCategorisedChanges()
		{
			Debug.Assert(this.Categories != null, "Categories collection is null");
			Debug.Assert(this.Changes != null, "Changes collection is null");

			var list = new List<IdentifiedChange>();

			// category not set.
			list.AddRange(this.Changes.Where(x => x.Priority == CategoryPriority.InvalidValue));

			// category that does not exist.
			list.AddRange(this.Changes.Where(x => x.Priority != CategoryPriority.InvalidValue
				&& !this.Categories.Any(c => c.Priority.Value == x.Priority)));

			return list;
		}
	}
}

﻿using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Analysis
{
	[DebuggerDisplay("{Name}")]
	public sealed class IdentifiedChangeCollection : IUniquelyIdentifiable
	{
		public IdentifiedChangeCollection()
		{
			this.Identifier = new Identifier().ToString();

			this.MetaBlocks = new List<string>();
			this.HeaderBlocks = new List<string>();
			this.FooterBlocks = new List<string>();
			this.SummaryBlocks = new Dictionary<string, string>();

			this.Categories = new HashSet<Category>();
			this.Changes = new List<IdentifiedChange>();

			this.Parents = new List<IDocumentLink>();
		}
		
		public string Identifier { get; set; }

		public List<IDocumentLink> Parents { get; private set; }

		public string Name { get; set; }

		// Review - are these necessary? 
		// may need to be on another object filled in immediately before
		// generating reports.
		public List<string> MetaBlocks { get; private set; }

		/// <summary>
		/// List of markup to put at beginning of each page content.
		/// </summary>
		public List<string> HeaderBlocks { get; private set; }

		public string Heading { get; set; }

		public string HeadingBlock { get; set; }

		public Dictionary<string, string> SummaryBlocks { get; private set; }

		public List<string> FooterBlocks { get; private set; }

		public HashSet<Category> Categories { get; private set; }

		public List<IdentifiedChange> Changes { get; private set; }

		private void Add(Category cat)
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

			// now look for category and flag up missing.
			Category c = change.Category;

            if (c != null)
            {
                if (this.Categories.Contains(c))
                {
                    int columnsInTable = c.Columns;
                    int columnsInData = 1;

                    if (change.Descriptor != null)
                    {
                        IDescriptor implementsIDescriptor = change.Descriptor as IDescriptor;

                        if (implementsIDescriptor != null)
                        {
                            columnsInData = implementsIDescriptor.Columns;
                        }
                    }

                    if (columnsInData != columnsInTable)
                        throw new Exception("Column mismatch " + c.Name);
                }

                if (!this.Categories.Contains(c))
                    this.Add(c);
            }

            this.Changes.Add(change);
		}

        public int CountChangesWithSeverity(Severity s)
        {
            return this.Changes.Count(x => x.Category.Severity >= s);
        }

        public ReadOnlyCollection<IdentifiedChange> ChangesWithSeverity(Severity s)
        {
            return new ReadOnlyCollection<IdentifiedChange>(this.Changes.Where(x => x.Category.Severity >= s).ToList());
        }

        public List<IdentifiedChange> ChangesInCategory(int priority) // and for a level ?
		{
			Debug.Assert(this.Categories != null, "Categories collection is null");
			Debug.Assert(this.Changes != null, "Changes collection is null");

			return this.Changes.Where(x => x.Priority == priority).ToList();
		}

        public List<IdentifiedChange> ChangesInCategory(string categoryName) // and for a level ???
        {
            Debug.Assert(this.Categories != null, "Categories collection is null");
            Debug.Assert(this.Changes != null, "Changes collection is null");

            return this.Changes.Where(x => x.Category.Name == categoryName).ToList();
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

        [Obsolete("No longer used")]
        public void SwitchCategory(Category from, Category to)
        {
            if (this.Categories.Contains(from))
                this.Categories.Add(to);

            foreach(var change in this.Changes)
            {
                if (change.Category.Name == from.Name && change.Category.Identifier == from.Identifier)
                {
                    change.Category = to;
                }
            }

            if (this.Categories.Contains(from))
                this.Categories.Remove(from);
        }

        [Obsolete("No longer used")]
        public void PurgeCategory(string categoryName)
        {
            this.Changes.RemoveAll(x => x.Category.Name == categoryName);
        }

		public void CopyMetaFrom(IdentifiedChangeCollection other)
		{
			this.MetaBlocks = new List<string>(other.MetaBlocks);
			this.HeaderBlocks = new List<string>(other.HeaderBlocks);
			this.FooterBlocks = new List<string>(other.FooterBlocks);
		}
	}
}

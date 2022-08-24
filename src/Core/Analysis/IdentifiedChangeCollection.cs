using NDifference.Reporting;
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

			// TODO needed here or only in the reporting.
			this.MetaBlocks = new List<string>();
			this.HeaderBlocks = new List<string>();
			this.FooterBlocks = new List<string>();
			this.SummaryBlocks = new Dictionary<string, string>();

			this.Changes = new List<IdentifiedChange>();

			// breadcrumbs back links to parents.
			this.Parents = new List<IDocumentLink>();
		}
		
		public string Identifier { get; set; }

		// link at report time?
		public List<IDocumentLink> Parents { get; private set; }

		// Name of type, assembly, document?
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

		public List<IdentifiedChange> Changes { get; private set; }

		public void Add(IdentifiedChange change)
		{
			Debug.Assert(this.Changes != null, "Changes collection is null");
			Debug.Assert(change != null, "Change is null");

            this.Changes.Add(change);
		}

        public int CountChangesWithSeverity(Severity minimumSeverity)
        {
            return this.Changes.Count(x => x.Severity >= minimumSeverity);
        }

        public ReadOnlyCollection<IdentifiedChange> ChangesWithSeverity(Severity minimumSeverity)
        {
            return new ReadOnlyCollection<IdentifiedChange>(this.Changes.Where(x => x.Severity >= minimumSeverity).ToList());
        }

        public List<IdentifiedChange> ChangesInCategory(int priority) 
		{
			Debug.Assert(this.Changes != null, "Changes collection is null");

			return this.Changes.Where(x => x.Priority == priority).ToList();
		}


		//public IEnumerable<int> Priorities()
  //      {

  //      }

        public List<IdentifiedChange> UnCategorisedChanges()
		{
			Debug.Assert(this.Changes != null, "Changes collection is null");

			return this.Changes.Where(x => x.Priority == CategoryPriority.Uncategorised).ToList();
		}

		public void CopyMetaFrom(IdentifiedChangeCollection other)
		{
			this.MetaBlocks = new List<string>(other.MetaBlocks);
			this.HeaderBlocks = new List<string>(other.HeaderBlocks);
			this.FooterBlocks = new List<string>(other.FooterBlocks);
		}
	}
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NDifference
{
	/// <summary>
	/// Represents a difference between two sets of objects.
	/// </summary>
	public class Delta<T, U> : IUniquelyIdentifiable
	{
		private List<T> added = new List<T>();

		private List<Pair<U>> changed = new List<Pair<U>>();

		private List<T> unchanged = new List<T>();

		private List<T> removed = new List<T>();

		public Delta()
		{
			this.Identifier = new Identifier().ToString();
		}

		public string Identifier { get; set; }

		/// <summary>
		/// Objects added to the new version.
		/// </summary>
		public Collection<T> Additions { get { return new Collection<T>(this.added); } }

		/// <summary>
		/// Objects that are common between versions but have failed comparisons.
		/// </summary>
		public Collection<Pair<U>> CandidateChanges { get { return new Collection<Pair<U>>(this.changed); } }

		/// <summary>
		/// Objects that have not changed between versions.
		/// </summary>
		public Collection<T> NoChanges { get { return new Collection<T>(this.unchanged); } }

		/// <summary>
		/// Objects that were in the original version and are not in the new version.
		/// </summary>
		public Collection<T> Removals { get { return new Collection<T>(this.removed); } }

		public void Added(T add)
		{
			Debug.Assert(add != null, "Added object cannot be null");
			this.added.Add(add);
		}

		public void Changed(Pair<U> pair)
		{
			Debug.Assert(pair != null, "Changed Pair cannot be null");
			Debug.Assert(pair.First != null, "Changed first object cannot be null");
			Debug.Assert(pair.Second != null, "Changed second object cannot be null");

			this.changed.Add(pair);
		}

		public void Unchanged(T same)
		{
			Debug.Assert(same != null, "Unchanged object cannot be null");
			this.unchanged.Add(same);
		}

		public void Removed(T itemRemoved)
		{
			Debug.Assert(itemRemoved != null, "Removed object cannot be null");
			this.removed.Add(itemRemoved);
		}
	}

}

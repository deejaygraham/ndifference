using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NDifference.Framework
{
	/// <summary>
	/// A "software product" with a number of published releases.
	/// </summary>
	[DebuggerDisplay("{Name}")]
	public sealed class Product
	{
		private List<ProductIncrement> increments = new List<ProductIncrement>();

		public Product()
		{
			this.FromIncrement = 0;
			this.ToIncrement = 1;
		}

		public Product(string name)
			: this()
		{
			Debug.Assert(!string.IsNullOrEmpty(name), "Name cannot be blank");

			this.Name = name;
		}

		public string Name { get; set; }

		/// <summary>
		/// All version/releases of this product.
		/// </summary>
		public ReadOnlyCollection<ProductIncrement> Increments
		{
			get
			{
				Debug.Assert(this.increments != null, "increment collection cannot be null");

				return new ReadOnlyCollection<ProductIncrement>(this.increments);
			}
		}

		/// <summary>
		/// Index of "from" increment.
		/// </summary>
		public int FromIncrement { get; set; }

		/// <summary>
		/// Index of "to" increment.
		/// </summary>
		public int ToIncrement { get; set; }

		public void Add(ProductIncrement increment)
		{
			Debug.Assert(increment != null, "ProductIncrement cannot be null");

			this.increments.Add(increment);
		}

		public void Clear()
		{
			this.increments.Clear();
		}

		public Pair<ProductIncrement> ComparedIncrements
		{
			get
			{
				Debug.Assert(this.FromIncrement >= 0, "From increment value is invalid");
				Debug.Assert(this.ToIncrement >= 0, "To increment value is invalid");
				Debug.Assert(this.FromIncrement != this.ToIncrement, "From and To increment values cannot be the same");

				Debug.Assert(this.increments != null, "ProductIncrement collection is null");
				Debug.Assert(this.increments.Count > 1, "Too few increments for comparison");
				Debug.Assert(this.ToIncrement < this.increments.Count, "To increment settings is too high");

				// REVIEW - return other indexes ???
				return Pair<ProductIncrement>.MakePair(this.increments[this.FromIncrement], this.increments[this.ToIncrement]);
			}
		}

	}
}

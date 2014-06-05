using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public void Add(ProductIncrement increment)
		{
			Debug.Assert(increment != null, "ProductIncrement cannot be null");

			this.increments.Add(increment);
		}

		public void Clear()
		{
			this.increments.Clear();
		}

		public ProductIncrementPair ComparedIncrements
		{
			get
			{
				Debug.Assert(this.increments != null, "ProductIncrement collection is null");
				Debug.Assert(this.increments.Count > 1, "Too few increments for comparison");

				// REVIEW - return other indexes ???
				return ProductIncrementPair.MakePair(this.increments[0], this.increments[1]);
			}
		}

	}
}

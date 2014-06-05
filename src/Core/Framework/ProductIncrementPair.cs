using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Framework
{
	public class ProductIncrementPair : Pair<ProductIncrement>
	{
		public static ProductIncrementPair MakePair(ProductIncrement first, ProductIncrement second)
		{
			Debug.Assert(!string.IsNullOrEmpty(first.Name), "First increment name cannot be blank");
			Debug.Assert(!string.IsNullOrEmpty(second.Name), "Second increment name cannot be blank");

			return new ProductIncrementPair
			{
				First = first,
				Second = second
			};
		}
	}

}

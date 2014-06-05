using NDifference.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class IdentifiedChangeCollectionFacts
	{
		[Fact]
		public void IdentifiedChangeCollection_Finds_Changes_For_Existing_Category()
		{
			var collection = new IdentifiedChangeCollection();

			collection.Add(new Category { Name = "C1", Priority = new CategoryPriority(1) });
			collection.Add(new IdentifiedChange { Priority = new CategoryPriority(1) });
			collection.Add(new IdentifiedChange { Priority = new CategoryPriority(1) });
			collection.Add(new IdentifiedChange { Priority = new CategoryPriority(1) });

			Assert.Equal(3, collection.ChangesInCategory(new CategoryPriority(1)).Count);
		}

		[Fact]
		public void IdentifiedChangeCollection_No_Changes_For_Empty_Category()
		{
			var collection = new IdentifiedChangeCollection();

			collection.Add(new Category { Name = "C1", Priority = new CategoryPriority(1) });
			collection.Add(new Category { Name = "C2", Priority = new CategoryPriority(2) });

			collection.Add(new IdentifiedChange { Priority = new CategoryPriority(1) });
			collection.Add(new IdentifiedChange { Priority = new CategoryPriority(1) });
			collection.Add(new IdentifiedChange { Priority = new CategoryPriority(1) });

			Assert.Equal(0, collection.ChangesInCategory(new CategoryPriority(2)).Count);
		}

		[Fact]
		public void IdentifiedChangeCollection_Finds_Uncategorised_Changes()
		{
			var collection = new IdentifiedChangeCollection();

			collection.Add(new Category { Name = "C1", Priority = new CategoryPriority(1) });
			collection.Add(new Category { Name = "C2", Priority = new CategoryPriority(2) });

			collection.Add(new IdentifiedChange());
			collection.Add(new IdentifiedChange());

			Assert.Equal(2, collection.UnCategorisedChanges().Count);
		}

		[Fact]
		public void IdentifiedChangeCollection_Finds_Miscategorised_Changes()
		{
			var collection = new IdentifiedChangeCollection();

			collection.Add(new Category { Name = "C1", Priority = new CategoryPriority(1) });
			collection.Add(new Category { Name = "C2", Priority = new CategoryPriority(2) });

			collection.Add(new IdentifiedChange());
			collection.Add(new IdentifiedChange());
			collection.Add(new IdentifiedChange { Priority = new CategoryPriority(3) });
			collection.Add(new IdentifiedChange { Priority = new CategoryPriority(4) });
			collection.Add(new IdentifiedChange { Priority = new CategoryPriority(5) });

			Assert.Equal(5, collection.UnCategorisedChanges().Count);
		}

	}
}

using NDifference.Analysis;
using NDifference.Inspection;
using Xunit;

namespace NDifference.UnitTests
{
	public class IdentifiedChangeCollectionFacts
	{
		//[Fact]
		//public void IdentifiedChangeCollection_Finds_Changes_For_Existing_Category()
		//{
		//	var collection = new IdentifiedChangeCollection();

		//	collection.Add(new Category { Name = "C1", Priority = new CategoryPriority(1) });
		//	collection.Add(new IdentifiedChange { Priority = 1 });
		//	collection.Add(new IdentifiedChange { Priority = 1 });
		//	collection.Add(new IdentifiedChange { Priority = 1 });

		//	Assert.Equal(3, collection.ChangesInCategory(1).Count);
		//}

		//[Fact]
		//public void IdentifiedChangeCollection_No_Changes_For_Empty_Category()
		//{
		//	var collection = new IdentifiedChangeCollection();

		//	collection.Add(new Category { Name = "C1", Priority = new CategoryPriority(1) });
		//	collection.Add(new Category { Name = "C2", Priority = new CategoryPriority(2) });

		//	collection.Add(new IdentifiedChange { Priority = 1 });
		//	collection.Add(new IdentifiedChange { Priority = 1 });
		//	collection.Add(new IdentifiedChange { Priority = 1 });

		//	Assert.Empty(collection.ChangesInCategory(2));
		//}

		//[Fact]
		//public void IdentifiedChangeCollection_Finds_Uncategorised_Changes()
		//{
		//	var collection = new IdentifiedChangeCollection();

		//	collection.Add(new Category { Name = "C1", Priority = new CategoryPriority(1) });
		//	collection.Add(new Category { Name = "C2", Priority = new CategoryPriority(2) });

		//	collection.Add(new IdentifiedChange());
		//	collection.Add(new IdentifiedChange());

		//	Assert.Equal(2, collection.UnCategorisedChanges().Count);
		//}

		//[Fact]
		//public void IdentifiedChangeCollection_Finds_Miscategorised_Changes()
		//{
		//	var collection = new IdentifiedChangeCollection();

		//	collection.Add(new Category { Name = "C1", Priority = new CategoryPriority(1) });
		//	collection.Add(new Category { Name = "C2", Priority = new CategoryPriority(2) });

		//	collection.Add(new IdentifiedChange());
		//	collection.Add(new IdentifiedChange());
		//	collection.Add(new IdentifiedChange { Priority = 3 });
		//	collection.Add(new IdentifiedChange { Priority = 4 });
		//	collection.Add(new IdentifiedChange { Priority = 5 });

		//	Assert.Equal(5, collection.UnCategorisedChanges().Count);
		//}

	}
}

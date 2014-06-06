using NDifference.Analysis;
using NDifference.Inspectors;
using NDifference.TypeSystem;
using System.Collections.Generic;
using Xunit;

namespace NDifference.UnitTests
{
	public class RemovedTypesInspectorFacts
	{
		[Fact]
		public void RemovedTypesInspector_Ignores_Identical_Lists()
		{
			var first = new List<ITypeInfo>();
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			var second = new List<ITypeInfo>();
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			ITypeCollectionInspector inspector = new RemovedTypesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(first, second, changes);

			Assert.Equal(0, changes.ChangesInCategory(WellKnownTypeCategories.AddedTypes.Priority).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownTypeCategories.RemovedTypes.Priority).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownTypeCategories.ChangedTypes.Priority).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownTypeCategories.UnchangedTypes.Priority).Count);
		}

		[Fact]
		public void RemovedTypesInspector_Identifies_Removed_Types()
		{
			var first = new List<ITypeInfo>();
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			var second = new List<ITypeInfo>();
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			ITypeCollectionInspector inspector = new RemovedTypesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(first, second, changes);

			Assert.Equal(0, changes.ChangesInCategory(WellKnownTypeCategories.AddedTypes.Priority).Count);
			Assert.Equal(1, changes.ChangesInCategory(WellKnownTypeCategories.RemovedTypes.Priority).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownTypeCategories.ChangedTypes.Priority).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownTypeCategories.UnchangedTypes.Priority).Count);
		}

	}

}

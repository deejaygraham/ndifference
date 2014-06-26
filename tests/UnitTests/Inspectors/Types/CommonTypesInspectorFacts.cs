using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Inspectors;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class CommonTypesInspectorFacts
	{
		[Fact]
		public void CommonTypesInspector_Ignores_Identical_Lists()
		{
			var first = new List<ITypeInfo>();
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			var second = new List<ITypeInfo>();
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			ITypeCollectionInspector inspector = new CommonTypesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedObjectModel.BuildFrom(first, second), changes);

			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.AddedTypes).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.RemovedTypes).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.ChangedTypes).Count);
		}

		[Fact]
		public void CommonTypesInspector_Identifies_Changed_Taxonomy()
		{
			var first = new List<ITypeInfo>();
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			var second = new List<ITypeInfo>();
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Enum });

			ITypeCollectionInspector inspector = new CommonTypesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedObjectModel.BuildFrom(first, second), changes);

			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.AddedTypes).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.RemovedTypes).Count);
			Assert.Equal(1, changes.ChangesInCategory(WellKnownChangePriorities.ChangedTypes).Count);
		}

	}

}

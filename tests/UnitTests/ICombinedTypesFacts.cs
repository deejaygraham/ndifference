using NDifference.Inspection;
using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NDifference.UnitTests
{
	public class ICombinedTypesFacts
	{
		[Fact]
		public void CombinedTypes_BuildFrom_Identical_Lists_All_Pairs_Are_Matched()
		{
			var first = new List<ITypeInfo>();
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			var second = new List<ITypeInfo>();
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			Assert.Empty(CombinedObjectModel.BuildFrom(first, second).InLaterOnly);
			Assert.Empty(CombinedObjectModel.BuildFrom(first, second).InEarlierOnly);

			Assert.Equal(3, CombinedObjectModel.BuildFrom(first, second).InCommon.Count());
		}

		[Fact]
		public void CombinedTypes_BuildFrom_Removed_Types_First_Of_Pairs_Are_Unmatched()
		{
			var first = new List<ITypeInfo>();
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			var second = new List<ITypeInfo>();
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			Assert.Empty(CombinedObjectModel.BuildFrom(first, second).InLaterOnly);
			Assert.Equal(2, CombinedObjectModel.BuildFrom(first, second).InEarlierOnly.Count());
			Assert.Single(CombinedObjectModel.BuildFrom(first, second).InCommon);
		}


		[Fact]
		public void CombinedTypes_BuildFrom_Added_Types_Second_Of_Pairs_Are_Unmatched()
		{
			var first = new List<ITypeInfo>();
			first.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			var second = new List<ITypeInfo>();
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.First", Name = "First", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Second", Name = "Second", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });
			second.Add(new PocoType { Access = AccessModifier.Public, FullName = "Example.Third", Name = "Third", Namespace = "Example", Taxonomy = TypeTaxonomy.Class });

			Assert.Equal(2, CombinedObjectModel.BuildFrom(first, second).InLaterOnly.Count());
			Assert.Empty(CombinedObjectModel.BuildFrom(first, second).InEarlierOnly);
			Assert.Single(CombinedObjectModel.BuildFrom(first, second).InCommon);
		}

	}
}

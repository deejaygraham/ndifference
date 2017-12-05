using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class IMatchFuzzilyFacts
	{
		[Fact]
		public void IMatchFuzzily_Collection_Contains_Fuzzy_Match()
		{
			var list = new List<FuzzyMatchItem>(Enumerable.Repeat(new FuzzyMatchItem(), 10));

			list.Add(new FuzzyMatchItem { AllowFuzzyMatch = true });

			Assert.True(list.ContainsFuzzyMatchFor(new FuzzyMatchItem()));
		}

		[Fact]
		public void IMatchFuzzily_Collection_Finds_Fuzzy_Match()
		{
			var list = new List<FuzzyMatchItem>(Enumerable.Repeat(new FuzzyMatchItem(), 10));

			list.Add(new FuzzyMatchItem { AllowFuzzyMatch = true });

			Assert.NotNull(list.FindFuzzyMatchFor(new FuzzyMatchItem()));
		}

		[Fact]
		public void IMatchFuzzily_Collection_Finds_All_Fuzzy_Matches()
		{
			var list = new List<FuzzyMatchItem>();

			list.Add(new FuzzyMatchItem { AllowFuzzyMatch = true });

			list.AddRange(Enumerable.Repeat(new FuzzyMatchItem(), 10));

			list.Add(new FuzzyMatchItem { AllowFuzzyMatch = true });

			Assert.Equal(2, list.FindAllFuzzyMatchesFor(new FuzzyMatchItem()).Count());
		}

		[Fact]
		public void IMatchFuzzily_Collection_Finds_All_Common_Fuzzy_Matches()
		{
			var list1 = new List<FuzzyMatchItem>(Enumerable.Repeat(new FuzzyMatchItem(), 10));
			list1.Add(new FuzzyMatchItem { AllowFuzzyMatch = true });

			var list2 = new List<FuzzyMatchItem>(Enumerable.Repeat(new FuzzyMatchItem(), 10));
			list2.Add(new FuzzyMatchItem { AllowFuzzyMatch = true });

			Assert.Single(list1.FuzzyInCommonWith(list2));
		}

		[Fact]
		public void IMatchFuzzily_Collection_Finds_Nothing_For_Distinct_Collections()
		{
			var list1 = new List<FuzzyMatchItem>(Enumerable.Repeat(new FuzzyMatchItem(), 10));

			var list2 = new List<FuzzyMatchItem>(Enumerable.Repeat(new FuzzyMatchItem(), 10));

			Assert.Empty(list1.FuzzyInCommonWith(list2));
		}

		[Fact]
		public void IMatchFuzzily_Collection_Returns_Find_In_Correct_Order()
		{
			var oldItem = new FuzzyMatchItem { AllowFuzzyMatch = true };
			var newItem = new FuzzyMatchItem { AllowFuzzyMatch = true };

			var oldList = new List<FuzzyMatchItem>();
			oldList.Add(oldItem);

			var newList = new List<FuzzyMatchItem>();
			newList.Add(newItem);

			var result = oldList.FuzzyInCommonWith(newList).First();

			Assert.Equal(oldItem, result.Item1);
			Assert.Equal(newItem, result.Item2);
		}

	}

	public class FuzzyMatchItem : IMatchFuzzily<FuzzyMatchItem>
	{
		public bool AllowFuzzyMatch { get; set; }

		public bool FuzzyMatches(FuzzyMatchItem other)
		{
			// match one item only...
			bool allow = this.AllowFuzzyMatch;

			if (allow)
			{
				this.AllowFuzzyMatch = false;
			}

			return allow;
		}
	}
}

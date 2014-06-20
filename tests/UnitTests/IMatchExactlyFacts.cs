using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class IMatchExactlyFacts
	{
		[Fact]
		public void IMatchExactly_Collection_Contains_Exact_Match()
		{
			var list = new List<TestExactly>(Enumerable.Repeat(new TestExactly(), 10));

			list.Add(new TestExactly { AllowMatch = true });

			Assert.True(list.ContainsExactMatchFor(new TestExactly()));
		}

		[Fact]
		public void IMatchExactly_Collection_Does_Not_Contain_InExact_Match()
		{
			var list = new List<TestExactly>(Enumerable.Repeat(new TestExactly(), 10));

			Assert.False(list.ContainsExactMatchFor(new TestExactly()));
		}

		[Fact]
		public void IMatchExactly_Collection_Finds_Exact_Match()
		{
			var list = new List<TestExactly>(Enumerable.Repeat(new TestExactly(), 10));
			list.Add(new TestExactly { AllowMatch = true });

			Assert.NotNull(list.FindExactMatchFor(new TestExactly()));
		}

		[Fact]
		public void IMatchExactly_Collection_Returns_Null_For_InExact_Match()
		{
			var list = new List<TestExactly>(Enumerable.Repeat(new TestExactly(), 10));

			Assert.Null(list.FindExactMatchFor(new TestExactly()));
		}
	}

	[DebuggerDisplay("{Id}")]
	public class TestExactly : IMatchExactly<TestExactly>
	{
		public bool AllowMatch { get; set; }

		public string Id { get; private set; }

		public TestExactly()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		public bool ExactlyMatches(TestExactly other)
		{
			return this.AllowMatch;
		}
	}
}

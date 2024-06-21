using NDifference.UnitTests.TestDataBuilders;
using Xunit;

namespace NDifference.UnitTests
{
    public class PocoTypeFacts
	{
        [Fact]
        public void PocoType_Hash_Is_Repeatable()
        {
            var p1 = TypeBuilder.Class().Named("World").InNamespace("Hello").Build();

            Assert.Equal(p1.CalculateHash(), p1.CalculateHash());
        }

		[Fact]
		public void PocoType_Hash_Is_Same_For_Identical_Objects()
		{
            var p1 = TypeBuilder.Class().Named("World").InNamespace("Hello").Build();
            var p2 = TypeBuilder.Class().Named("World").InNamespace("Hello").Build();

			Assert.Equal(p2.CalculateHash(), p1.CalculateHash());
		}

		[Fact]
		public void PocoType_Hash_Is_Different_For_Different_Objects()
		{
            var p1 = TypeBuilder.Class().Named("World").InNamespace("Hello").IsInternal().Build();
            var p2 = TypeBuilder.Class().Named("World").InNamespace("Hello").Build();

			Assert.NotEqual(p2.CalculateHash(), p1.CalculateHash());
		}
	}
}

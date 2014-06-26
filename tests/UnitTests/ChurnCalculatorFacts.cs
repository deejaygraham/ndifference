using NDifference.Analysis;
using Xunit;

namespace NDifference.UnitTests
{
	public class ChurnCalculatorFacts
	{
		[Fact]
		public void ChurnCalculator_No_Changes_Reports_Zero_Churn()
		{
			ChurnCalculator c = new ChurnCalculator(new TestChurn { Total = 55, Removed = 0, Added = 0, Changed = 0 });

			Assert.Equal(0, c.Calculate());
		}


		[Fact]
		public void ChurnCalculator_No_Changes_No_Classes_Reports_Zero_Churn()
		{
			ChurnCalculator c = new ChurnCalculator(new TestChurn { Total = 0, Removed = 0, Added = 0, Changed = 0 });

			Assert.Equal(0, c.Calculate());
		}

		[Fact]
		public void ChurnCalculator_Total_Changes_Reports_100_Percent_Churn()
		{
			ChurnCalculator c = new ChurnCalculator(new TestChurn { Total = 110, Removed = 55, Added = 55, Changed = 0 });

			Assert.Equal(100, c.Calculate());
		}

		[Fact]
		public void ChurnCalculator_Partial_Change_Reports_Percent_Churn()
		{
			ChurnCalculator c = new ChurnCalculator(new TestChurn { Total = 31, Removed = 1, Added = 2, Changed = 3 });

			Assert.Equal(29, c.Calculate());
		}

	}

	public class TestChurn : IChurnable
	{
		public int Total { get; set; }

		public int Removed { get; set; }

		public int Added { get; set; }

		public int Changed { get; set; }
	}
}

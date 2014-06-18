using NDifference.Inspectors;
using Xunit;

namespace NDifference.UnitTests
{
	public class ConstantsRemovedFacts
	{
		[Fact]
		public void ConstantsRemoved_Identifies_When_Removing_Constant()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsRemoved_Identifies_When_Const_Removed_From_Field()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsRemoved_Ignores_When_No_Constants()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsRemoved_Ignores_When_Constants_Same()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsRemoved_Ignores_When_Constant_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"")
										.WithConstant("int", "MyInt", "1");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}
	}

}

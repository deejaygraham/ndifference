using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class ConstantsAddedFacts
	{
		[Fact]
		public void ConstantsAdded_Identifies_When_Adding_New_Constant()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsAdded())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsAdded_Identifies_When_Existing_Field_Made_Constant()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsAdded())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsAdded_Ignores_When_No_Constants()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsAdded())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsAdded_Ignores_When_Constants_Same()
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
				.InspectedBy(new ConstantsAdded())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsAdded_Ignores_When_Constant_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsAdded())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}
	}
}

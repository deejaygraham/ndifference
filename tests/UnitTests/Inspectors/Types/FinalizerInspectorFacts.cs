using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests.Inspectors.Types
{
	public class FinalizerInspectorFacts
	{

		[Fact]
		public void FinalizerAdded_Identifies_When_Finalizer_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithFinalizer();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FinalizerAdded())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void FinalizerAdded_Ignores_When_Finalizer_Unchanged()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithFinalizer();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithFinalizer();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FinalizerAdded())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void FinalizerRemoved_Identifies_When_Finalizer_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithFinalizer();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FinalizerRemoved())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void FinalizerRemoved_Ignores_When_Finalizer_Unchanged()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithFinalizer();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithFinalizer();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FinalizerRemoved())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

	}
}

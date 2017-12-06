using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class ClassSealingInspectorFacts
	{
		[Fact]
		public void ClassDerivationInspector_Identifies_When_Class_Is_Sealed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.IsSealed();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ClassSealingInspector())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void ClassDerivationInspector_Ignores_When_Class_Is_Always_Sealed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.IsSealed();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.IsSealed();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ClassSealingInspector())
				.Build();

			Assert.Empty(delta.Changes);
		}
	}
}

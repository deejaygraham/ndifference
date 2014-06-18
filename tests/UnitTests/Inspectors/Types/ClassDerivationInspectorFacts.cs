using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class ClassDerivationInspectorFacts
	{
		[Fact]
		public void ClassDerivationInspector_Identifies_When_Base_Class_Changes()
		{
			var oldSuperClassBuilder = CompilableClassBuilder.PublicClass()
				.Named("BaseAccount");

			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.DerivedFrom("BaseAccount");

			string oldCode = BoilerplateCodeBuilder.BuildFor(new IBuildToCode[] { oldSuperClassBuilder, oldClassBuilder });

			var newSuperClassBuilder = CompilableClassBuilder.PublicClass()
				.Named("NewBaseAccount");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.DerivedFrom("NewBaseAccount");

			string newCode = BoilerplateCodeBuilder.BuildFor(new IBuildToCode[] { newSuperClassBuilder, newClassBuilder });

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldCode)
				.To(newCode)
				.InspectedBy(new ClassDerivationInspector())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void ClassDerivationInspector_Ignores_When_Base_Class_Unchanged()
		{
			var superClassBuilder = CompilableClassBuilder.PublicClass()
				.Named("BaseAccount");

			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.DerivedFrom("BaseAccount");

			string oldCode = BoilerplateCodeBuilder.BuildFor(new IBuildToCode[] { superClassBuilder, oldClassBuilder });

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.DerivedFrom("BaseAccount");


			string newCode = BoilerplateCodeBuilder.BuildFor(new IBuildToCode[] { superClassBuilder, newClassBuilder });

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldCode)
				.To(newCode)
				.InspectedBy(new ClassDerivationInspector())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void ClassDerivationInspector_Ignores_When_No_Base_Class()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ClassDerivationInspector())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void ClassDerivationInspector_Identifies_When_Base_Class_Removed()
		{
			var superClassBuilder = CompilableClassBuilder.PublicClass()
				.Named("BaseAccount");

			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.DerivedFrom("BaseAccount");

			string oldCode = BoilerplateCodeBuilder.BuildFor(new IBuildToCode[] { superClassBuilder, oldClassBuilder });

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			string newCode = BoilerplateCodeBuilder.BuildFor(newClassBuilder);

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldCode)
				.To(newCode)
				.InspectedBy(new ClassDerivationInspector())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}
	}
}

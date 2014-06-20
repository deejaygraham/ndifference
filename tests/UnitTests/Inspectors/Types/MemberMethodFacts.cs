using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class MemberMethodFacts
	{
		[Fact]
		public void MethodsChanged_Ignores_Identical_Classes()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsChanged())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void MethodsChanged_Making_Method_Abstract_Is_Identified()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.IsAbstract()
							.WithMethod("public abstract void DoStuff ();");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsChanged())
				.Build();

			Assert.Equal(1, delta.Changes.Count);

		}

	}
}

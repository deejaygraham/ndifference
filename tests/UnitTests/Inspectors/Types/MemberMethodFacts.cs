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
		public void MethodsAdded_Adding_Method_Is_Identified()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }")
							.WithMethod("public void DoStuff2 () { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsAdded())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void MethodsRemoved_Removing_Method_Is_Identified()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }")
							.WithMethod("public void DoStuff2 () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsRemoved())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void MethodsObsolete_Identifies_Obsolete_Method()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("[Obsolete] public void DoStuff () { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsObsolete())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

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

		[Fact]
		public void MethodsChanged_Making_Method_Static_Is_Identified()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public static void DoStuff () { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsChanged())
				.Build();

			Assert.Equal(1, delta.Changes.Count);

		}

	}
}

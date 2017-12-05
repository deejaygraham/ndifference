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

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void MethodsAdded_Adding_Protected_Method_Is_Identified()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }")
							.WithMethod("protected void DoStuff2 () { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsAdded())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void MethodsChanged_Changing_Method_Public_To_Protected_Is_Identified()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("protected void DoStuff () { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsChanged())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void MethodsChanged_Changing_Method_Protected_To_Public_Is_Identified()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("protected void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsChanged())
				.Build();

			Assert.Single(delta.Changes);
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

			Assert.Single(delta.Changes);
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

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void MethodsChanged_Ignores_Identical_Void_Method()
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

			Assert.Empty(delta.Changes);
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

			Assert.Single(delta.Changes);

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

			Assert.Single(delta.Changes);

		}

		[Fact]
		public void MethodsChanged_Changing_Return_Type_Is_Identified()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public int DoStuff () { return 1; }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsChanged())
				.Build();

			Assert.Single(delta.Changes);

		}


		[Fact]
		public void MethodsChanged_Making_Method_Virtual_Is_Identified()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public void DoStuff () { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithMethod("public virtual void DoStuff () { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new MethodsChanged())
				.Build();

			Assert.Single(delta.Changes);

		}

	}
}

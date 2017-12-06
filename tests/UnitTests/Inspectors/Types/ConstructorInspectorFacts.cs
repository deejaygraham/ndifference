using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests.Inspectors.Types
{
	public class ConstructorInspectorFacts
	{
		[Fact]
		public void InstanceConstructorsChanged_Identifies_When_Constructor_Argument_Changes_Type()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("public Account(int money) { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("public Account(double money) { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new InstanceConstructorsInspector())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void InstanceConstructorsChanged_Identifies_When_Constructor_Argument_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("public Account() { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("public Account(double money) { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new InstanceConstructorsInspector())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void InstanceConstructorsChanged_Identifies_When_Constructor_Adds_Arguments()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("public Account(double money) { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("public Account(double money, string currencyName) { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new InstanceConstructorsInspector())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void InstanceConstructorsChanged_Identifies_When_New_Constructor_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor()
										.WithConstructor("public Account(double money, string currencyName) { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new InstanceConstructorsInspector())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void InstanceConstructorsChanged_Identifies_When_Constructor_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor()
										.WithConstructor("public Account(double money, string currencyName) { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new InstanceConstructorsInspector())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void InstanceConstructorsObsoleted_Identifies_When_Constructor_Made_Obsolete()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("public Account(int money) { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("[Obsolete] public Account(int money) { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new InstanceConstructorsObsolete())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void InstanceConstructorsObsoleted_Ignores_When_Constructor_Not_Obsolete()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("public Account(int money) { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstructor("public Account(int money) { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new InstanceConstructorsObsolete())
				.Build();

			Assert.Empty(delta.Changes);
		}


		[Fact]
		public void StaticConstructorsAdded_Identifies_When_Static_Constructor_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithStaticConstructor();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new StaticConstructorAdded())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void StaticConstructorsAdded_Ignores_When_Static_Constructor_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithStaticConstructor();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new StaticConstructorAdded())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void StaticConstructorsAdded_Ignores_When_Instance_Constructor_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor()
										.WithConstructor("public Account(int money) { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new StaticConstructorAdded())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void StaticConstructorsAdded_Ignores_When_Instance_Constructor_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor()
										.WithConstructor("public Account(int money) { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new StaticConstructorAdded())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void StaticConstructorsRemoved_Identifies_When_Static_Constructor_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithStaticConstructor();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new StaticConstructorRemoved())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void StaticConstructorsRemoved_Ignores_When_Static_Constructor_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithStaticConstructor();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new StaticConstructorRemoved())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void StaticConstructorsRemoved_Ignores_When_Instance_Constructor_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor()
										.WithConstructor("public Account(int money) { }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new StaticConstructorRemoved())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void StaticConstructorsRemoved_Ignores_When_Instance_Constructor_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor()
										.WithConstructor("public Account(int money) { }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithDefaultConstructor();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new StaticConstructorRemoved())
				.Build();

			Assert.Empty(delta.Changes);
		}

	}
}

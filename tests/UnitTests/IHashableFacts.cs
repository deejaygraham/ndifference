using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class IHashableFacts
	{
		[Fact]
		public void IHashable_ClassDefinition_Is_Hashable()
		{
			Assert.NotNull(new ClassDefinition().CalculateHash());
		}

		[Fact]
		public void IHashable_Hash_Function_Gives_Repeatable_Results()
		{
			var list = new List<ExampleHashable>();

			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());
			list.Add(new ExampleHashable());

			string firstValue = list.First().CalculateHash();

			foreach(var item in list)
			{
				Assert.Equal(firstValue, item.CalculateHash());
			}
		}

		[Fact]
		public void IHashable_EnumDefinition_Is_Hashable()
		{
			Assert.NotNull(new EnumDefinition().CalculateHash());
		}

		[Fact]
		public void IHashable_InterfaceDefinition_Is_Hashable()
		{
			Assert.NotNull(new InterfaceDefinition().CalculateHash());
		}

		[Fact]
		public void IHashable_Classes_Different_Constructors_Calculate_Different_Hash_Values()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithDefaultConstructor()
							.WithConstructor("public Account(int money) { }")
							.WithConstant("public int ConstantValue = 10; ")
							.WithProperty("public string AutoProp { get; set; }")
							.WithMethod("public override string ToString() { return string.Empty; } ");

			IAssemblyReflector oldReflector = AssemblyReflectorBuilder.Introspection()
					.Code(oldClassBuilder.Build())
					.Build();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithDefaultConstructor()
							.WithConstructor("public Account(double money) { }")
							.WithConstant("public int ConstantValue = 10; ")
							.WithProperty("public string AutoProp { get; set; }")
							.WithMethod("public override string ToString() { return string.Empty; } ");

			IAssemblyReflector newReflector = AssemblyReflectorBuilder.Introspection()
					.Code(newClassBuilder.Build())
					.Build();

			var oldType = oldReflector.GetTypes().First();
			var newType = newReflector.GetTypes().First();

			Assert.NotEqual(oldType.CalculateHash(), newType.CalculateHash());
		}

		[Fact]
		public void IHashable_Identical_Classes_Calculate_Same_Hash_Value()
		{
			var oldSourceCode = CompilableClassBuilder.PublicClass()
							.Named("Account")
							.WithDefaultConstructor()
							.WithConstructor("public Account(int money) { }")
							.WithConstant("public int ConstantValue = 10; ")
							.WithProperty("public string AutoProp { get; set; }")
							.WithMethod("public override string ToString() { return string.Empty; } ")
							.Build();

			var newSourceCode = CompilableClassBuilder.PublicClass()
				.Named("Account")
				.WithDefaultConstructor()
				.WithConstructor("public Account(int money) { }")
				.WithConstant("public int ConstantValue = 10; ")
				.WithProperty("public string AutoProp { get; set; }")
				.WithMethod("public override string ToString() { return string.Empty; } ")
				.Build();

			Assert.Equal(oldSourceCode, newSourceCode);
			
			IAssemblyReflector oldReflector = AssemblyReflectorBuilder.Introspection()
					.Code(oldSourceCode)
					.Build();

			IAssemblyReflector newReflector = AssemblyReflectorBuilder.Introspection()
					.Code(newSourceCode)
					.Build();

			var oldType = oldReflector.GetTypes().First();
			var newType = newReflector.GetTypes().First();

			Assert.Equal(oldType.CalculateHash(), newType.CalculateHash());
		}
	}

	[Serializable]
	public class ExampleHashable : IHashable
	{
		public ExampleHashable()
		{

		}

		public ExampleHashable(string name)
		{
			this.Name = name;
		}

		public string Ident { get; set; }

		public string Name { get; set; }

		public bool NamesMatch(ExampleHashable eh)
		{
			return this.Name.CompareTo(eh.Name) == 0;
		}

		public string CalculateHash()
		{
			return this.GetHash<SHA1Managed>();
		}
	}
}

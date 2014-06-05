using NDifference.Reflection;
using NDifference.TypeSystem;
using System.Linq;
using Xunit;

namespace NDifference.UnitTests
{
	public class CecilAssemblyReflectorFacts
	{
		[Fact]
		public void CecilAssemblyReflector_Loads_Simple_Class_From_Assembly()
		{
			using (var ofc = new OnTheFlyCompiler())
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Entity")
					.WithField("int serialNo = 100;")
					.WithDefaultConstructor()
					.WithMethod("public int GetSerialNo() { return serialNo; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();
				
				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Equal(1, types.Count());

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);
				Assert.Equal("Entity", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Generic_Class_From_Assembly()
		{
			using (var ofc = new OnTheFlyCompiler())
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Entity<T>")
					.WithField("private T obj;")
					.WithMethod("public void SetValue(T val) { obj = val; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Equal(1, types.Count());
				
				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);
				Assert.Equal("Entity<T>", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Simple_Interface_From_Assembly()
		{
			using (var ofc = new OnTheFlyCompiler())
			{
				IBuildToCode code = CompilableInterfaceBuilder.PublicInterface()
					.Named("IEntity")
					.WithProperty("int Value { get; set; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Equal(1, types.Count());

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Interface, info.Taxonomy);
				Assert.Equal("IEntity", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Generic_Interface_From_Assembly()
		{
			using (var ofc = new OnTheFlyCompiler())
			{
				IBuildToCode code = CompilableInterfaceBuilder.PublicInterface()
					.Named("IEntity<T>")
					.WithProperty("T Value { get; set; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Equal(1, types.Count());

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Interface, info.Taxonomy);
				Assert.Equal("IEntity<T>", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Enum_From_Assembly()
		{
			using (var ofc = new OnTheFlyCompiler())
			{
				IBuildToCode code = CompilableEnumBuilder.PublicEnum()
					.Named("EntityValues")
					.WithValue("First")
					.WithValue("Second")
					.WithValue("Third");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Equal(1, types.Count());

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Enum, info.Taxonomy);
				Assert.Equal("EntityValues", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Assembly_Info()
		{
			using (var ofc = new OnTheFlyCompiler())
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Entity");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var info = reflector.GetAssemblyInfo();

				Assert.NotNull(info);
				Assert.False(string.IsNullOrEmpty(info.Identifier));
				Assert.False(string.IsNullOrEmpty(info.Name));
				Assert.False(string.IsNullOrEmpty(info.Architecture));
				Assert.False(string.IsNullOrEmpty(info.RuntimeVersion));
			}
		}

	}
}

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
            var ofc = new OnTheFlyCompiler();
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

				Assert.Single(types);

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);
				Assert.Equal("Entity", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Abstract_Method_In_Class_Is_Abstract()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Entity")
					.IsAbstract()
					.WithDefaultConstructor()
					.WithMethod("public abstract int GetSerialNo();");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Single(types);

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);

				ClassDefinition cd = info as ClassDefinition;

				Assert.True(cd.AllMethods[0].IsAbstract);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Virtual_Method_In_Class_Is_Virtual()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Entity")
					.WithDefaultConstructor()
					.WithMethod("public virtual int GetSerialNo() { return 0; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Single(types);

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);

				ClassDefinition cd = info as ClassDefinition;

				Assert.True(cd.AllMethods[0].IsVirtual);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Generic_Class_From_Assembly()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Entity<T>")
					.WithField("private T obj;")
					.WithMethod("public void SetValue(T val) { obj = val; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Single(types);
				
				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);
				Assert.Equal("Entity<T>", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Simple_Interface_From_Assembly()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableInterfaceBuilder.PublicInterface()
					.Named("IEntity")
					.WithProperty("int Value { get; set; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

                Assert.Single(types);

                var info = types.First();

				Assert.Equal(TypeTaxonomy.Interface, info.Taxonomy);
				Assert.Equal("IEntity", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Interface_Methods_Are_Not_Virtual()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableInterfaceBuilder.PublicInterface()
					.Named("IEntity")
					.WithMethod("int CalcValue();");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

                Assert.Single(types);

                var info = types.First();

				Assert.Equal(TypeTaxonomy.Interface, info.Taxonomy);

				InterfaceDefinition id = info as InterfaceDefinition;

				Assert.False(id.AllMethods[0].IsVirtual);
				Assert.False(id.AllMethods[0].IsAbstract);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Generic_Interface_From_Assembly()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableInterfaceBuilder.PublicInterface()
					.Named("IEntity<T>")
					.WithProperty("T Value { get; set; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

                Assert.Single(types);

                var info = types.First();

				Assert.Equal(TypeTaxonomy.Interface, info.Taxonomy);
				Assert.Equal("IEntity<T>", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Enum_From_Assembly()
        {
            var ofc = new OnTheFlyCompiler();
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

                Assert.Single(types);

                var info = types.First();

				Assert.Equal(TypeTaxonomy.Enum, info.Taxonomy);
				Assert.Equal("EntityValues", info.FullName);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Assembly_Info()
        {
            var ofc = new OnTheFlyCompiler();
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

		[Fact]
		public void CecilAssemblyReflector_Load_Class_With_Generic_Type_In_Method()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Example")
					.WithMethod("public List<string> GetValues() { return new List<string>(); }");

				IBuildToCode wholeFile = CompilableSourceBuilder.CompilableCode()
					.UsingDefaultNamespaces()
					.HasNamespace("ex1")
					.Includes(code);

				var compiled = ofc.Compile(wholeFile);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

                Assert.Single(types);

                var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);
				ClassDefinition cd = info as ClassDefinition;

				Assert.NotNull(cd);
                Assert.Single(cd.AllMethods);
				
				MemberMethod mm = cd.AllMethods.First() as MemberMethod;
				Assert.Equal("List<String>", mm.ReturnType.Type.ToString());
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Loads_Internal_Class()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableClassBuilder.InternalClass()
					.Named("Example");

				IBuildToCode wholeFile = CompilableSourceBuilder.CompilableCode()
					.UsingDefaultNamespaces()
					.HasNamespace("ex1")
					.Includes(code);

				var compiled = ofc.Compile(wholeFile);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

                Assert.Single(types);

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);
				ClassDefinition cd = info as ClassDefinition;

				Assert.NotNull(cd);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Ignores_Internal_Class_When_Looking_For_Publics()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableClassBuilder.InternalClass()
					.Named("Example");

				IBuildToCode wholeFile = CompilableSourceBuilder.CompilableCode()
					.UsingDefaultNamespaces()
					.HasNamespace("ex1")
					.Includes(code);

				var compiled = ofc.Compile(wholeFile);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes(AssemblyReflectionOption.Public);

				Assert.Empty(types);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Finds_Public_Methods_In_Class()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Entity")
					.WithDefaultConstructor()
					.WithMethod("public int GetSerialNo() { return 0; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

                Assert.Single(types);

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);

				ClassDefinition cd = info as ClassDefinition;

				Assert.Single(cd.AllMethods);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Finds_Protected_Methods_In_Class()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Entity")
					.WithDefaultConstructor()
					.WithMethod("protected int GetSerialNo() { return 0; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Single(types);

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);

				ClassDefinition cd = info as ClassDefinition;

				Assert.Single(cd.AllMethods);
			}
		}

		[Fact]
		public void CecilAssemblyReflector_Ignores_Private_Methods_In_Class()
        {
            var ofc = new OnTheFlyCompiler();
			{
				IBuildToCode code = CompilableClassBuilder.PublicClass()
					.Named("Entity")
					.WithDefaultConstructor()
					.WithMethod("private int GetSerialNo() { return 0; }");

				var compiled = ofc.Compile(code);

				var factory = new CecilReflectorFactory();

				var reflector = factory.LoadAssembly(compiled.Path);

				var types = reflector.GetTypes();

				Assert.Single(types);

				var info = types.First();

				Assert.Equal(TypeTaxonomy.Class, info.Taxonomy);

				ClassDefinition cd = info as ClassDefinition;

				Assert.Empty(cd.AllMethods);
			}
		}
	}
}

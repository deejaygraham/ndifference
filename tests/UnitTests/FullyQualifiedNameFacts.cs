using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class FullyQualifiedNameFacts
	{
		[Fact]
		public void FullyQualifiedName_Class_Name_Only_Namespace_Is_Blank()
		{
			var fqn = new FullyQualifiedName("World");

			Assert.Equal(string.Empty, fqn.ContainingNamespace.ToString());
		}

		[Fact]
		public void FullyQualifiedName_Class_Name_Only_Name_Is_Type_Only()
		{
			const string ClassName = "World";

			var fqn = new FullyQualifiedName(ClassName);

			Assert.Equal(ClassName, fqn.Type.ToString());
		}

		[Fact]
		public void FullyQualifiedName_Class_Name_Only_ToString_Returns_Type_Only()
		{
			const string ClassName = "World";

			var fqn = new FullyQualifiedName(ClassName);

			Assert.Equal(ClassName, fqn.ToString());
		}

		[Fact]
		public void FullyQualifiedName_Namespace_And_Type_ToString_Returns_FullName()
		{
			const string NamespaceName = "Hello";
			const string ClassName = "World";
			const string FullName = NamespaceName + "." + ClassName;

			var fqn = new FullyQualifiedName(FullName);

			Assert.Equal(FullName, fqn.ToString());
			Assert.Equal(NamespaceName, fqn.ContainingNamespace.ToString());
			Assert.Equal(ClassName, fqn.Type.ToString());
		}

		[Fact]
		public void FullyQualifiedName_Two_Classes_Same_Namespace_Are_Not_Equal()
		{
			Assert.NotEqual(new FullyQualifiedName("System.Int32"), new FullyQualifiedName("System.Boolean"));
		}

		[Fact]
		public void FullyQualifiedName_Two_Classes_Different_Namespace_Same_Type_Are_Not_Equal()
		{
			Assert.NotEqual(new FullyQualifiedName("System.Int32"), new FullyQualifiedName("Foo.Int32"));
		}

		[Fact]
		public void FullyQualifiedName_Two_Classes_Same_Namespace_Same_Type_Are_Equal()
		{
			Assert.Equal(new FullyQualifiedName("System.String"), new FullyQualifiedName("System.String"));
		}

	}
}

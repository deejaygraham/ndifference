using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDifference.SourceFormatting;
using Xunit;

namespace NDifference.UnitTests
{
    public class SourceFormattingFacts
    {
        [Fact]
        public void FullyQualifiedName_System_Type_Forces_Lowercase_Omitting_Namespace()
        {
            var fqn = new FullyQualifiedName("System.String");

            Assert.Equal("string", fqn.ToCode().ToPlainText());
        }

        [Fact]
        public void FullyQualifiedName_Custom_Type_Retains_Case_And_Namespace()
        {
            const string typeName = "Xyz.Flooble";
            var fqn = new FullyQualifiedName(typeName);

            Assert.Equal(typeName, fqn.ToCode().ToPlainText());
        }

        [Fact]
        public void Signature_ToPlainText_Contains_Method_Name_And_Arg_Types()
        {
            var parameters = new List<Parameter>
            {
                new Parameter(new FullyQualifiedName("string")),
                new Parameter(new FullyQualifiedName("int")),
                new Parameter(new FullyQualifiedName("bool"))
            };

            var signature = new Signature("Export", parameters);

            Assert.Equal("Export(string, int, bool)", signature.ToCode().ToPlainText());
        }

        [Fact]
        public void FieldMethod_ToString_Is_Formatted_On_Single_Line()
        {
            var method = new MemberMethod();
            method.Accessibility = MemberAccessibility.Public;
            method.IsAbstract = false;
            method.IsStatic = true;
            method.ReturnType = new FullyQualifiedName("bool");

            var parameters = new List<Parameter>
            {
                new Parameter(new FullyQualifiedName("string")),
                new Parameter(new FullyQualifiedName("int")),
                new Parameter(new FullyQualifiedName("bool"))
            };

            method.Signature = new Signature("Export", parameters);
            
            Assert.Equal("static bool Export(string, int, bool)", method.ToCode().ToPlainText());
        }
	}
}

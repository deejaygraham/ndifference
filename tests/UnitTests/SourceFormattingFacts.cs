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
        public void Constant_Is_Spaced_Correctly()
        {
            var c = new Constant
            {
                Accessibility = MemberAccessibility.Public,
                Name = "PI",
                ConstantType = new FullyQualifiedName("System.Decimal")
            };

            Assert.Equal("const decimal PI", c.ToCode().ToPlainText());
        }

        [Fact]
        public void Field_Is_Spaced_Correctly()
        {
            var field = new MemberField()
            {
                Accessibility = MemberAccessibility.Public,
                Name = "_InitialiseDate",
                FieldType = new FullyQualifiedName("System.DateTime")
            };

            Assert.Equal("System.DateTime _InitialiseDate", field.ToCode().ToPlainText());
        }

        [Fact]
        public void Read_Only_Property_Is_Spaced_Between_Tokens()
        {
            var property = new MemberProperty
            {
                Name = "Discrepancy",
                GetterAccessibility = MemberAccessibility.Public,
                PropertyType = new FullyQualifiedName("System.Decimal")
            };

            Assert.Equal("decimal Discrepancy { get; }", property.ToCode().ToPlainText());
        }

        [Fact]
        public void Writable_Property_Is_Spaced_Between_Tokens()
        {
            var property = new MemberProperty
            {
                Name = "Name",
                GetterAccessibility = MemberAccessibility.Public,
                SetterAccessibility = MemberAccessibility.Public,
                PropertyType = new FullyQualifiedName("System.String")
            };

            Assert.Equal("string Name { get; set; }", property.ToCode().ToPlainText());
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

        [Fact]
        public void Class_Definition_ToString_Is_Formatted_On_Single_Line()
        {
            var cd = new ClassDefinition
            {
                Name = "PaymentAuthenticator",
                Access = AccessModifier.Public,
                Implements = new List<FullyQualifiedName>
                {
                    new FullyQualifiedName("IEnumerable"),
                    new FullyQualifiedName("IDisposable")
                },

                InheritsFrom = new FullyQualifiedName("BasePaymentAuthenticator")
            };

            Assert.Equal("public class PaymentAuthenticator : BasePaymentAuthenticator, IEnumerable, IDisposable", cd.ToCode().ToPlainText());
        }
    }
}

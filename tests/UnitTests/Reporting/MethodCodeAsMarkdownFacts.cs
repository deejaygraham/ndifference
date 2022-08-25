using NDifference.Reporting;
using NDifference.TypeSystem;
using Xunit;

namespace NDifference.UnitTests
{
    public class MethodCodeAsMarkdownFacts
    {
        [Fact]
        public void Method_With_Generic_Return_Type_Preserves_Angle_Brackets()
        {
            var mm = new MemberMethod
            {
                IsStatic = true,
                Signature = new Signature("ConvertToMetadata"),
                ReturnType = new FullyQualifiedName("System.Collections.Generic.IDictionary<String,String>")
            };

            var code = mm.ToCode();

            var md = new ReportAsMarkdown();
            string formatted = md.Format(code);

            Assert.Contains("IDictionary<String,String>", formatted);
        }
    }
}


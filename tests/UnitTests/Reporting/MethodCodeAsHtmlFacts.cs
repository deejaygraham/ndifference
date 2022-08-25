using NDifference.Reporting;
using NDifference.TypeSystem;
using Xunit;

namespace NDifference.UnitTests
{
    public class MethodCodeAsHtmlFacts
    {
        [Fact]
        public void Method_With_Generic_Return_Type_Is_Escaped()
        {
            var html4 = new ReportAsHtml4();

            var mm = new MemberMethod();
            mm.IsStatic = true;
            mm.Signature = new Signature("ConvertToMetadata");
            mm.ReturnType = new FullyQualifiedName("System.Collections.Generic.IDictionary<String,String>");

            var tn = mm.ToCode();

            string formatted = html4.Format(tn);

            Assert.Contains("IDictionary&lt;String,String&gt;", formatted);
        }
    }
}

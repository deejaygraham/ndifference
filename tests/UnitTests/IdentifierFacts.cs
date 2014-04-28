using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
    public class IdentifierFacts
    {
		public void Identifier_Two_Instances_Are_Not_Equal()
		{
			Assert.NotEqual(new Identifier(), new Identifier());
		}

		public void Identifier_Two_Instances_Can_Be_Forced_Equality()
		{
			var first = new Identifier();
			var second = new Identifier(first);

			Assert.Equal(first, second);
		}
    }
}

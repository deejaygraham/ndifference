using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class PocoTypeFacts
	{
		[Fact]
		public void PocoType_Hash_Is_Same_For_Identical_Objects()
		{
			var p1 = new PocoType { FullName = "Hello.World" };
			var p2 = new PocoType { FullName = "Hello.World" };

			Assert.Equal(p2.Hash(), p1.Hash());
		}

		[Fact]
		public void PocoType_Hash_Is_Different_For_Different_Objects()
		{
			var p1 = new PocoType { FullName = "Hello.World", Access = AccessModifier.Internal };
			var p2 = new PocoType { FullName = "Hello.World", Access = AccessModifier.Public };

			Assert.NotEqual(p2.Hash(), p1.Hash());
		}
	}
}

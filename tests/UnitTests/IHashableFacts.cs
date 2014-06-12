using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public void IHashable_EnumDefinition_Is_Hashable()
		{
			Assert.NotNull(new EnumDefinition().CalculateHash());
		}

		[Fact]
		public void IHashable_InterfaceDefinition_Is_Hashable()
		{
			Assert.NotNull(new InterfaceDefinition().CalculateHash());
		}

	}
}

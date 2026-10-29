using System;
using System.Collections.Generic;
using NDifference.Inspection;
using Xunit;

namespace NDifference.UnitTests
{
	public class ObjectInstantiatorFacts
	{
		[Fact]
		public void ObjectInstantiator_Creates_Discovered_Plugin()
		{
			var provider = new StubRuntimeTypeProvider(typeof(TestPlugin));
			var instantiator = new ObjectInstantiator<ITestPlugin>();

			var plugins = instantiator.CreateTypesImplementingInterface(provider);

			Assert.Single(plugins);
			Assert.IsType<TestPlugin>(plugins[0]);
		}

		[Fact]
		public void ObjectInstantiator_Rejects_Type_That_Does_Not_Implement_Plugin_Interface()
		{
			var provider = new StubRuntimeTypeProvider(typeof(NotAPlugin));
			var instantiator = new ObjectInstantiator<ITestPlugin>();

			var exception = Assert.Throws<InvalidOperationException>(
				() => instantiator.CreateTypesImplementingInterface(provider));

			Assert.Contains(typeof(NotAPlugin).FullName, exception.Message);
			Assert.Contains(typeof(ITestPlugin).FullName, exception.Message);
		}

		public interface ITestPlugin
		{
		}

		public class TestPlugin : ITestPlugin
		{
		}

		public class NotAPlugin
		{
		}

		private class StubRuntimeTypeProvider : IRuntimeTypeProvider
		{
			private readonly Type pluginType;

			public StubRuntimeTypeProvider(Type pluginType)
			{
				this.pluginType = pluginType;
			}

			public IEnumerable<Type> GetRuntimeTypesImplementing(Type requiredInterface)
			{
				return new[] { this.pluginType };
			}
		}
	}
}

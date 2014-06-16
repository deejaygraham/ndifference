using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.UnitTests.TestDataBuilders
{
	public static class BoilerplateCodeBuilder
	{
		private const string DefaultNamespace = "Example";

		public static string BuildFor(IBuildToCode singleBuildable)
		{
			return BuildFor(DefaultNamespace, singleBuildable);
		}

		public static string BuildFor(IBuildToCode[] multiBuildable)
		{
			return BuildFor(DefaultNamespace, multiBuildable);
		}

		public static string BuildFor(string namespaceName, IBuildToCode singleBuildable)
		{
			return CompilableSourceBuilder.CompilableCode()
								.UsingDefaultNamespaces()
								.HasNamespace(namespaceName)
								.Includes(singleBuildable)
								.Build();
		}

		public static string BuildFor(string namespaceName, IBuildToCode[] multiBuildable)
		{
			var builder = CompilableSourceBuilder.CompilableCode()
								.UsingDefaultNamespaces()
								.HasNamespace(namespaceName);

			foreach (IBuildToCode buildable in multiBuildable)
			{
				builder.Includes(buildable);
			}

			return builder.Build();
		}

	}

}

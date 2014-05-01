using System.Collections.Generic;
using System.Text;

namespace NDifference.UnitTests
{
	/// <summary>
	/// Generates framework C# code for an arbitrary number of child 
	/// code builders
	/// </summary>
	public class CompilableSourceBuilder : IBuildToCode
	{
		private List<string> usings = new List<string>();
		private string namespaceName;
		private List<IBuildToCode> typeBuilders = new List<IBuildToCode>();

		public static CompilableSourceBuilder CompilableCode()
		{
			return new CompilableSourceBuilder();
		}

		public CompilableSourceBuilder UsingNamespace(string usingReference)
		{
			this.usings.Add(usingReference);

			return this;
		}

		public CompilableSourceBuilder UsingDefaultNamespaces()
		{
			this.usings.Add("System");
			this.usings.Add("System.Collections.Generic");
			this.usings.Add("System.Text");

			return this;
		}

		public CompilableSourceBuilder HasNamespace(string namespaceName)
		{
			this.namespaceName = namespaceName;

			return this;
		}

		public CompilableSourceBuilder Includes(IBuildToCode builder)
		{
			this.typeBuilders.Add(builder);

			return this;
		}

		public string Build()
		{
			var builder = new StringBuilder();

			foreach (var u in this.usings)
			{
				builder.AppendFormat("using {0};", u);
				builder.AppendLine();
			}

			builder.AppendLine();

			builder.AppendFormat("namespace {0}", this.namespaceName);
			builder.AppendLine();
			builder.AppendLine("{");

			foreach (var tb in this.typeBuilders)
			{
				builder.AppendLine(tb.Build());
				builder.AppendLine();
			}

			builder.AppendLine("}"); // end of namespace

			return builder.ToString();
		}
	}

}

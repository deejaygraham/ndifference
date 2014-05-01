using System.Collections.Generic;
using System.Text;

namespace NDifference.UnitTests
{
	public class UsingStatementBuilder : IBuildToCode
	{
		private List<string> usings = new List<string>();

		public static UsingStatementBuilder UsingStatements()
		{
			return new UsingStatementBuilder();
		}

		public UsingStatementBuilder UsingNamespace(string usingReference)
		{
			this.usings.Add(usingReference);

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

			return builder.ToString();
		}
	}
}

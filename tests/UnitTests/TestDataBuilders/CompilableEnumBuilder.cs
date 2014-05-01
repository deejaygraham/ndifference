using System.Collections.Generic;
using System.Text;

namespace NDifference.UnitTests
{
	/// <summary>
	/// Generates C# code for a single enum.
	/// </summary>
	public class CompilableEnumBuilder : IBuildToCode
	{
		private string name = "TestEnum";
		private long currentValue = 0;

		private Dictionary<string, long> allowedValues = new Dictionary<string, long>();

		public static CompilableEnumBuilder PublicEnum()
		{
			return new CompilableEnumBuilder();
		}

		public CompilableEnumBuilder Named(string name)
		{
			this.name = name;

			return this;
		}

		public CompilableEnumBuilder WithValue(string name)
		{
			this.allowedValues.Add(name, this.currentValue++);

			return this;
		}

		public CompilableEnumBuilder WithValue(string name, long value)
		{
			this.currentValue = value;
			this.allowedValues.Add(name, this.currentValue++);

			return this;
		}

		public string Build()
		{
			var builder = new StringBuilder();

			builder.AppendLine("public enum " + this.name);
			builder.AppendLine("{");

			List<string> values = new List<string>();

			foreach (var key in this.allowedValues.Keys)
			{
				values.Add(string.Format("{0} = {1}", key, this.allowedValues[key]));
			}

			builder.AppendLine(string.Join(",", values));

			builder.AppendLine("}"); // end of enum

			return builder.ToString();
		}
	}
}

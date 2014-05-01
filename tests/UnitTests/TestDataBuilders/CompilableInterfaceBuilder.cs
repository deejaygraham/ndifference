using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDifference.UnitTests
{
	/// <summary>
	/// Generates C# code for a single interface.
	/// </summary>
	public class CompilableInterfaceBuilder : IBuildToCode
	{
		private string className;
		private List<string> properties = new List<string>();
		private List<string> methods = new List<string>();
		private bool isInternal = false;
		private bool obsolete = false;
		private string obsoleteText = string.Empty;
		private List<string> interfaces = new List<string>();

		public static CompilableInterfaceBuilder PublicInterface()
		{
			return new CompilableInterfaceBuilder();
		}

		public static CompilableInterfaceBuilder InternalInterface()
		{
			return new CompilableInterfaceBuilder
			{
				isInternal = true
			};
		}

		public CompilableInterfaceBuilder Named(string cn)
		{
			this.className = cn;

			return this;
		}

		public CompilableInterfaceBuilder Implementing(string interfaceName)
		{
			this.interfaces.Add(interfaceName);

			return this;
		}

		public CompilableInterfaceBuilder WithProperty(string propertyCode)
		{
			this.properties.Add(propertyCode);

			return this;
		}

		public CompilableInterfaceBuilder WithMethod(string methodCode)
		{
			this.methods.Add(methodCode);

			return this;
		}

		public CompilableInterfaceBuilder IsObsolete()
		{
			this.obsolete = true;

			return this;
		}

		public CompilableInterfaceBuilder IsObsolete(string text)
		{
			this.obsolete = true;
			this.obsoleteText = text;

			return this;
		}

		public CompilableInterfaceBuilder IsInternal()
		{
			this.isInternal = true;

			return this;
		}

		public string Build()
		{
			var builder = new StringBuilder();

			builder.AppendLine("\t// Interface ");

			if (this.obsolete)
			{
				builder.Append("\t[Obsolete");
				if (!string.IsNullOrEmpty(this.obsoleteText))
				{
					builder.AppendFormat("(\"{0}\")", this.obsoleteText);
				}

				builder.AppendLine("]");
			}

			builder.Append("\t");

			if (this.isInternal)
			{
				builder.Append("internal ");
			}
			else
			{
				builder.Append("public ");
			}

			builder.AppendFormat("interface {0}", this.className);

			if (this.interfaces.Any())
			{
				builder.Append(" : ");
				builder.Append(string.Join(", ", this.interfaces));
			}

			builder.AppendLine();
			builder.AppendLine("\t{");

			this.properties.GenerateMemberCode(builder, "Properties");
			this.methods.GenerateMemberCode(builder, "Methods");

			builder.AppendLine("\t}");
			builder.AppendLine("\t// End of Interface ");

			return builder.ToString();
		}
	}
}

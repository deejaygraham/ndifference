using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public class SourceCodeFieldBuilder
	{
		private TypeNameTag typeName;
		private IdentifierTag identifier;

		public static SourceCodeFieldBuilder Field()
		{
			return new SourceCodeFieldBuilder();
		}

		public SourceCodeFieldBuilder Named(string name)
		{
			this.identifier = new IdentifierTag(name);
			return this;
		}

		public SourceCodeFieldBuilder WithType(string fieldType)
		{
			this.typeName = new TypeNameTag(fieldType);
			return this;
		}

		public SourceCode Build()
		{
			SourceCode code = new SourceCode();

			code.Add(this.typeName);
			code.Add(new WhitespaceTag());
			code.Add(this.identifier);

			return code;
		}
	}
}

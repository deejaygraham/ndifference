using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public class SourceCodeMethodBuilder
	{
		private IdentifierTag identifier;
		private TypeNameTag typeName;
		private List<TypeNameTag> argumentList = new List<TypeNameTag>();
		private KeywordTag isAbstract = null;

		public static SourceCodeMethodBuilder Method()
		{
			return new SourceCodeMethodBuilder();
		}

		public SourceCodeMethodBuilder Named(string name)
		{
			this.identifier = new IdentifierTag(name);
			return this;
		}

		public SourceCodeMethodBuilder WithReturnType(string returnType)
		{
			this.typeName = new TypeNameTag(returnType);
			return this;
		}

		public SourceCodeMethodBuilder IsAbstract()
		{
			this.isAbstract = new KeywordTag("abstract");
			return this;
		}

		public SourceCodeMethodBuilder IsAbstract(bool flag)
		{
			this.isAbstract = flag ? new KeywordTag("abstract") : null;
			return this;
		}

		public SourceCodeMethodBuilder WithArg(string argType)
		{
			this.argumentList.Add(new TypeNameTag(argType));

			return this;
		}

		public SourceCode Build()
		{
			SourceCode code = new SourceCode();

			if (this.typeName != null)
			{
				code.Add(this.typeName);
                code.Add(new WhitespaceTag());
			}

			if (this.isAbstract != null)
			{
				code.Add(this.isAbstract);
				code.Add(new WhitespaceTag());
			}

			code.Add(this.identifier);

            code.Add(new PunctuationTag("("));

			for (int i = 0; i < this.argumentList.Count; ++i)
			{
				code.Add(this.argumentList[i]);

				if (i < this.argumentList.Count - 1)
				{
					code.Add(new PunctuationTag(","));
				}
			}

			code.Add(new PunctuationTag(")"));

			return code;
		}
	}
}

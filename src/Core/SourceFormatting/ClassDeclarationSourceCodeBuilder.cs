using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public class ClassDeclarationSourceCodeBuilder
	{
		private IdentifierTag identifier;

		public static ClassDeclarationSourceCodeBuilder Class()
		{
			return new ClassDeclarationSourceCodeBuilder();
		}

		//public static ClassDeclarationSourceCodeBuilder Class(ClassDeclaration cdec)
		//{
		//	ClassDeclarationSourceCodeBuilder builder = new ClassDeclarationSourceCodeBuilder();

		//   // builder.Named(cdec.TypeName.Name);

		//	return builder;
		//}

		public ClassDeclarationSourceCodeBuilder Named(string name)
		{
			this.identifier = new IdentifierTag(name);
			return this;
		}

		public SourceCode Build()
		{
			SourceCode code = new SourceCode();

			code.Add(new KeywordTag("public"));

			//if (this.IsAbstract)
			//{
			//    code.Add(new KeywordTag("abstract"));
			//}

			//if (this.IsSealed)
			//{
			//    code.Add(new KeywordTag("sealed"));
			//}

			//code.Add(new KeywordTag("class"));
			//code.Add(new TypeNameTag(this.TypeName.FullName));
			return code;
		}
	}
}

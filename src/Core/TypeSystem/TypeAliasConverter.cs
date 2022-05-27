using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	static class TypeAliasConverter
	{
		private static Dictionary<string, string> _aliasLookup = new Dictionary<string, string>
			{
			    { "System.Object", "object" },
                { "System.Boolean", "bool" },
				{ "System.String", "string" },
				{ "System.Double", "double" },
				{ "System.Decimal", "decimal" },
				{ "System.Int32", "int" },
				{ "System.Int64", "long" },
				{ "System.Void", "void" }
			};
	
		public static string Convert(FullyQualifiedName fqn)
		{
			string fullyQualifiedText = fqn.ToString();

			if (_aliasLookup.ContainsKey(fqn.ToString()))
				return _aliasLookup[fullyQualifiedText];

			return fullyQualifiedText;
		}
	}
}

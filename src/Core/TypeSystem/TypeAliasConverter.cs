using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	public static class TypeAliasConverter
	{
		private static Dictionary<string, string> _aliasLookup = new Dictionary<string, string>();
	
		static TypeAliasConverter()
		{
			_aliasLookup.Add("System.Object", "object");
			_aliasLookup.Add("System.Boolean", "bool");
			_aliasLookup.Add("System.String", "string");
			_aliasLookup.Add("System.Double", "double");
			_aliasLookup.Add("System.Decimal", "decimal");
			_aliasLookup.Add("System.Int32", "int");
			_aliasLookup.Add("System.Int64", "long");
			_aliasLookup.Add("System.Void", "void");
		}

		public static string Convert(FullyQualifiedName fqn)
		{
			string fullyQualifiedText = fqn.ToString();

			if (_aliasLookup.ContainsKey(fqn.ToString()))
				return _aliasLookup[fullyQualifiedText];

			return fullyQualifiedText;
		}
	}
}

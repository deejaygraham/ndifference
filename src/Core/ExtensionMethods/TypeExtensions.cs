using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	public static class TypeExtensions
	{
		public static ITypeInfo ToTypeInfo(this Type t)
		{
			Debug.Assert(t != null, "Type cannot be null");

			return new PocoType
			{
				Taxonomy = t.ToTaxonomy(),
				FullName = t.FullName, // need to convert to friendly name...
				Name = t.Name,
				Namespace = t.Namespace,
				Assembly = t.Assembly.FullName
			};
		}

		public static TypeTaxonomy ToTaxonomy (this Type t)
		{
			TypeTaxonomy kind = TypeTaxonomy.Unknown;

			if (t.IsClass)
				kind = TypeTaxonomy.Class;
			else if (t.IsInterface)
				kind = TypeTaxonomy.Interface;
			else if (t.IsEnum)
				kind = TypeTaxonomy.Enum;

			return kind;
		}
	}
}

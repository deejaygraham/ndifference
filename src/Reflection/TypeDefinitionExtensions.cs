using Mono.Cecil;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reflection
{
	public static class TypeDefinitionExtensions
	{
		/// <summary>
		/// Is this type derived from something other than system.object?.
		/// </summary>
		/// <param name="discovered">The type definition.</param>
		/// <returns>True if derived from anything but system.object.</returns>
		public static bool HasSuperClass(this TypeDefinition discovered)
		{
			const string BaseObjectName = "System.Object";

			return discovered.BaseType != null && discovered.BaseType.FullName != BaseObjectName;
		}

		public static TypeTaxonomy ToTaxonomy(this TypeDefinition td)
		{
			Debug.Assert(td != null, "Type definition cannot be blank");

			TypeTaxonomy kind = TypeTaxonomy.Unknown;

			// check is enum first - enums are enum and class according to cecil.
			if (td.IsEnum)
			{
				kind = TypeTaxonomy.Enum;
			}
			else if (td.IsClass)
			{
				kind = TypeTaxonomy.Class;
			}
			else if (td.IsInterface)
			{
				kind = TypeTaxonomy.Interface;
			}
			else if (td.IsEnum)
			{
				kind = TypeTaxonomy.Enum;
			}

			return kind;
		}

		public static ITypeInfo ToTypeInfo(this TypeDefinition td)
		{
			Debug.Assert(td != null, "Type definition cannot be blank");

			var fqn = new FullyQualifiedName(td.FriendlyName());

			var t = new PocoType
			{
				Taxonomy = td.ToTaxonomy(),
				FullName = fqn.ToString(),
				Name = fqn.Type.ToString(),
				Namespace = fqn.ContainingNamespace.ToString(),
				Assembly = td.Module.Assembly.FullName
			};

			return t;
		}

	}
}

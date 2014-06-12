using Mono.Cecil;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NDifference.Reflection
{
    public static class CecilExtensionMethods
    {
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

		public static string FriendlyName(this TypeReference td)
		{
			if (td.HasGenericParameters)
			{
				return MakeGenericFriendlyName(td);
			}

			return td.FullName;
		}

		public static bool IsInternalType(this TypeDefinition td)
		{
			const string NetModuleType = "<Module>";
			const string ExtenionsMethodSignature = "<"; // extension methods seem to show up as className/<blah>

			return td.Name.StartsWith(NetModuleType) 
				|| (td.Name.StartsWith(ExtenionsMethodSignature)
				&& td.IsNestedPrivate);
		}

        public static bool IsInPublicApi(this FieldDefinition field)
        {
            return field.IsPublic() || field.IsProtected();
        }

        /// <summary>
        /// Extension method to determine if this field definition is public.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>True if it is public.</returns>
        public static bool IsPublic(this FieldDefinition field)
        {
            if (field.IsSpecialName)
            {
                return false;
            }

            return field.IsPublic;
        }

        public static bool IsProtected(this FieldDefinition field)
        {
            if (field.IsSpecialName)
            {
                return false;
            }

            return field.IsFamily || field.IsFamilyAndAssembly || field.IsFamilyOrAssembly;
        }

		public static long ConstantValue(this FieldDefinition field)
		{
			long value = 0;

			try
			{
				value = Convert.ToInt64(field.Constant);
			}
			catch (OverflowException)
			{
				value = 0;
			}
			catch (InvalidCastException)
			{
				value = 0;
			}

			return value;
		}

		private static string MakeGenericFriendlyName(TypeReference td)
		{
			var builder = new StringBuilder();

			if (!string.IsNullOrEmpty(td.Namespace))
				builder.AppendFormat("{0}.", td.Namespace);

			string className = StripGenericNameSuffix(td);

			builder.Append(className);

			const string OpenTag = "<";
			const string CloseTag = ">";

			builder.Append(OpenTag);

			builder.Append(CreateGenericParameterList(td.GenericParameters));

			builder.Append(CloseTag);

			return builder.ToString();
		}

		private static string StripGenericNameSuffix(TypeReference td)
		{
			const char Tilde = '`';

			int tildePosition = td.Name.IndexOf(Tilde);

			if (tildePosition > 0)
			{
				return td.Name.Substring(0, tildePosition);
			}

			return td.Name;
		}

		private static string CreateGenericParameterList(IEnumerable<GenericParameter> parameters)
		{
			var list = new List<string>();

			foreach (var p in parameters)
			{
				list.Add(p.Name);
			}

			const string CommaDelimiter = ",";

			return string.Join(CommaDelimiter, list);
		}
    }
}

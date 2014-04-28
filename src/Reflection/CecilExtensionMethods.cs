using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Reflection
{
    public static class CecilExtensionMethods
    {
		public static string FriendlyName(this TypeDefinition td)
		{
			if (td.HasGenericParameters)
			{
				return MakeGenericFriendlyName(td);
			}

			return td.FullName;
		}

		public static bool IsInternalName(this TypeDefinition td)
		{
			const string NetModuleType = "<Module>";
			const string ExtenionsMethodSignature = "<"; // extension methods seem to show up as className/<blah>

			return td.Name.StartsWith(NetModuleType) 
				|| (td.Name.StartsWith(ExtenionsMethodSignature)
				&& td.IsNestedPrivate);
		}

		private static string MakeGenericFriendlyName(TypeDefinition td)
		{
			var builder = new StringBuilder();

			string className = StripGenericNameSuffix(td);

			builder.AppendFormat("{0}.{1}", td.Namespace, className);

			const string OpenTag = "<";
			const string CloseTag = ">";

			builder.Append(OpenTag);

			builder.Append(CreateGenericParameterList(td.GenericParameters));

			builder.Append(CloseTag);

			return builder.ToString();
		}

		private static string StripGenericNameSuffix(TypeDefinition td)
		{
			Debug.Assert(td != null);

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

			return String.Join(CommaDelimiter, list);
		}
    }
}

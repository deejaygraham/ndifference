using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reflection
{
	public static class TypeReferenceExtensions
	{
		public static string FriendlyName(this TypeReference td)
		{
			if (td.IsGenericInstance)
			{
				return MakeGenericFriendlyName(td);
			}
			else if (td.HasGenericParameters)
			{
				return MakeGenericFriendlyName(td);
			}

			return td.FullName;
		}

		public static bool IsPublicInterface(this TypeReference candidateInterace)
		{
			TypeDefinition interfaceDefinition = candidateInterace as TypeDefinition;

			if (interfaceDefinition != null)
			{
				return interfaceDefinition.IsPublic;
			}

			return false;
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

			if (td.HasGenericParameters)
			{
				builder.Append(CreateGenericParameterList(td.GenericParameters));
			}
			else if (td.IsGenericInstance)
			{
				GenericInstanceType git = td as GenericInstanceType;

				if (git != null)
				{
					//git.
					builder.Append(CreateParameterList(git.GenericArguments));
				}
			}


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

		private static string CreateParameterList(IEnumerable<TypeReference> parameters)
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

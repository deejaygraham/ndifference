using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reflection
{
	public static class MethodDefinitionExtensions
	{
		/// <summary>
		/// Is this method suitable for an interface declaration ?.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns>True if it is an interface method.</returns>
		public static bool IsInterfaceMethod(this MethodDefinition method)
		{
			if (method.IsSpecialName || method.IsConstructor)
			{
				return false;
			}

			return method.IsPublic;
		}

		public static bool IsInPublicApi(this MethodDefinition method)
		{
			return method.IsPublic || method.IsProtected();
		}

		public static bool IsIncluded(this MethodDefinition method)
		{
			return method.IsPublic || method.IsProtected();
		}

		public static bool IsProtected(this MethodDefinition method)
		{
			return method.IsFamily || method.IsFamilyAndAssembly || method.IsFamilyOrAssembly;
		}

		public static bool IsFinalizer(this MethodDefinition method)
		{
			return method.Name == "Finalize" && method.Parameters.Count == 0;
		}
	}

}

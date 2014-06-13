using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reflection
{
	/// <summary>
	/// Factory type extension to build a declarations from equivalent mono definition objects.
	/// </summary>
	public static class FieldDefinitionExtensions
	{
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
	}
}

using Mono.Cecil;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reflection
{
	public static class PropertyDefinitionExtensions
	{
		/// <summary>
		/// Does this property have a public getter.
		/// </summary>
		/// <param name="property">The property.</param>
		public static MemberAccessibility GetterAccessibility(this PropertyDefinition property)
		{
			if (property.GetMethod != null)
			{
				if (property.GetMethod.IsPublic)
					return MemberAccessibility.Public;
				else if (property.GetMethod.IsProtected())
					return MemberAccessibility.Protected;
			}

			return MemberAccessibility.Private;
		}

		/// <summary>
		/// Does this property have a public setter.
		/// </summary>
		/// <param name="property">The property.</param>
		public static MemberAccessibility SetterAccessibility(this PropertyDefinition property)
		{
			if (property.SetMethod != null)
			{
				if (property.SetMethod.IsPublic)
					return MemberAccessibility.Public;
				else if (property.SetMethod.IsProtected())
					return MemberAccessibility.Protected;
			}

			return MemberAccessibility.Private;
		}

		/// <summary>
		/// Does this property have a public getter.
		/// </summary>
		/// <param name="property">The property.</param>
		/// <returns>True if a public getter exists.</returns>
		public static bool HasPublicGetter(this PropertyDefinition property)
		{
			if (property.GetMethod != null)
			{
				return property.GetMethod.IsPublic;
			}

			return false;
		}

		/// <summary>
		/// Does this property have a public setter.
		/// </summary>
		/// <param name="property">The property.</param>
		/// <returns>True if a public setter exists.</returns>
		public static bool HasPublicSetter(this PropertyDefinition property)
		{
			if (property.SetMethod != null)
			{
				return property.SetMethod.IsPublic;
			}

			return false;
		}
	}
}

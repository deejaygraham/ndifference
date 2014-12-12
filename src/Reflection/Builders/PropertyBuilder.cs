using Mono.Cecil;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reflection.Builders
{
	public class PropertyBuilder
	{
		public void BuildFrom(TypeDefinition discovered, IReferenceTypeDefinition building)
		{
			Debug.Assert(discovered != null, "Type definition is null");
			Debug.Assert(building != null, "Class definition is null");

			if (discovered.HasProperties)
			{
				foreach (var property in discovered.Properties.Where(x =>
					x.GetterAccessibility() != MemberAccessibility.Private
					|| x.SetterAccessibility() != MemberAccessibility.Private))
				{
					building.AllProperties.Add(BuildFrom(property));
				}
			}
		}

		public MemberProperty BuildFrom(PropertyDefinition pd)
		{
			Debug.Assert(pd != null, "Property definition is null");

			var builtProperty = new MemberProperty();

			builtProperty.Name = pd.Name;
			builtProperty.PropertyType = new FullyQualifiedName(pd.PropertyType.FriendlyName());

			builtProperty.GetterAccessibility = pd.GetterAccessibility();
			builtProperty.SetterAccessibility = pd.SetterAccessibility();

			// sort of public
			if (builtProperty.GetterAccessibility == MemberAccessibility.Public || builtProperty.SetterAccessibility == MemberAccessibility.Public)
				builtProperty.Accessibility = MemberAccessibility.Public;
			else
				builtProperty.Accessibility = MemberAccessibility.Protected;


			var obsBuilder = new ObsoleteBuilder();
			obsBuilder.BuildFrom(pd, builtProperty);

			return builtProperty;
		}

	}

}

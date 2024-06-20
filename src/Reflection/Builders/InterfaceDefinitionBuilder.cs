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
	public class InterfaceDefinitionBuilder
	{
		public InterfaceDefinition BuildFrom(TypeDefinition discovered)
		{
			Debug.Assert(discovered != null, "TypeDefinition must be set");
			Debug.Assert(discovered.IsInterface, "TypeDefinition must be an interface");
//			Debug.Assert(discovered.IsPublic, "TypeDefinition must be a public type");

			var fqn = new FullyQualifiedName(discovered.FriendlyName());

			var id = new InterfaceDefinition
			{
				FullName = discovered.FriendlyName(),
				Name = fqn.Type.ToString(),
				Namespace = fqn.ContainingNamespace.ToString(),
				Assembly = discovered.Module.Assembly.Name.Name,
				Access = discovered.IsPublic ? AccessModifier.Public : AccessModifier.Internal
			};

			var eventBuilder = new MemberEventBuilder();
			eventBuilder.BuildFrom(discovered, id);

			var propBuilder = new PropertyBuilder();
			propBuilder.BuildFrom(discovered, id);

			var methodBuilder = new MethodBuilder
			{
				SuppressAbstractModifier = true,
				SuppressVirtualModifier = true
			};

			methodBuilder.BuildFrom(discovered, id);

			if (discovered.HasInterfaces)
			{
				foreach (var inter in discovered.Interfaces)
                {
                    var interfaceType = inter.InterfaceType;

					if (interfaceType.IsPublicInterface())
					{
						id.Implements.Add(new FullyQualifiedName(interfaceType.FriendlyName()));
					}
				}
			}

			var obsoleteBuilder = new ObsoleteBuilder();
			obsoleteBuilder.BuildFrom(discovered, id);

			Debug.Assert(id.Taxonomy == TypeTaxonomy.Interface, "Not an interface");
			Debug.Assert(!String.IsNullOrEmpty(id.Name), "No name defined");
			Debug.Assert(!String.IsNullOrEmpty(id.Assembly), "No assembly defined");

			return id;
		}
	}
}

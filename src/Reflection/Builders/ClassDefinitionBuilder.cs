using Mono.Cecil;
using NDifference.TypeSystem;
using System;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Reflection.Builders
{
	public class ClassDefinitionBuilder
	{
		public ClassDefinition BuildFrom(TypeDefinition discovered)
		{
			Debug.Assert(discovered != null, "TypeDefinition must be set");
			Debug.Assert(discovered.IsClass, "TypeDefinition must be a class");
//			Debug.Assert(discovered.IsPublic, "TypeDefinition must be a public type");
			
			var fqn = new FullyQualifiedName(discovered.FriendlyName());

			var cd = new ClassDefinition
			{
				FullName = discovered.FriendlyName(),
				Name = fqn.Type.ToString(),
				Namespace = fqn.ContainingNamespace.ToString(),
				Assembly = discovered.Module.Assembly.Name.Name,
				Access = discovered.IsPublic ? AccessModifier.Public : AccessModifier.Internal,
				IsAbstract = discovered.IsAbstract,
				IsSealed = discovered.IsSealed
			};

			var eventBuilder = new MemberEventBuilder();
			eventBuilder.BuildFrom(discovered, cd);

			var fieldBuilder = new FieldBuilder();
			fieldBuilder.BuildFrom(discovered, cd);

			var propertyBuilder = new PropertyBuilder();
			propertyBuilder.BuildFrom(discovered, cd);

			var methodBuilder = new MethodBuilder();
			methodBuilder.BuildFrom(discovered, cd);

			if (discovered.HasSuperClass())
			{
				cd.InheritsFrom = new FullyQualifiedName(discovered.BaseType.FriendlyName());
			}

			if (discovered.HasInterfaces)
			{
				foreach (var inter in discovered.Interfaces)
				{
                    var interfaceType = inter.InterfaceType;

                    if (interfaceType.IsPublicInterface())
                    {
                        cd.Implements.Add(new FullyQualifiedName(interfaceType.FriendlyName()));
                    }
                }
			}

			var obsoleteBuilder = new ObsoleteBuilder();
			obsoleteBuilder.BuildFrom(discovered, cd);

			Debug.Assert(cd.Taxonomy == TypeTaxonomy.Class, "Not a class");
			Debug.Assert(!String.IsNullOrEmpty(cd.Name), "No name defined");
			Debug.Assert(!String.IsNullOrEmpty(cd.Assembly), "No assembly defined");
			
			return cd;
		}
	}
}

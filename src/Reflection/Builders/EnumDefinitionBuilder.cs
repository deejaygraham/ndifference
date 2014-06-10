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
	public class EnumDefinitionBuilder
	{
		public EnumDefinition BuildFrom(TypeDefinition discovered)
		{
			Debug.Assert(discovered != null, "TypeDefinition must be set");
			Debug.Assert(discovered.IsEnum, "TypeDefinition must be an enum");

			var fqn = new FullyQualifiedName(discovered.FriendlyName());

			var ed = new EnumDefinition
			{
				FullName = discovered.FriendlyName(),
				Name = fqn.Type.ToString(),
				Namespace = fqn.ContainingNamespace.ToString(),
				Assembly = discovered.Module.Assembly.FullName,
				Access = discovered.IsPublic ? AccessModifier.Public : AccessModifier.Internal
			};

			if (discovered.HasFields)
			{
				foreach (var field in discovered.Fields.Where(x => x.IsPublic()))
				{
					ed.Add(field.Name, field.ConstantValue());
				}
			}

			var obsoleteBuilder = new ObsoleteBuilder();
			obsoleteBuilder.BuildFrom(discovered, ed);

			Debug.Assert(ed.Taxonomy == TypeTaxonomy.Enum, "Not an enum");
			Debug.Assert(!String.IsNullOrEmpty(ed.Name), "No name defined");
			Debug.Assert(!String.IsNullOrEmpty(ed.Assembly), "No assembly defined");

			return ed;
		}
	}
}

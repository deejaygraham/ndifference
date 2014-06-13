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
	public class FieldBuilder
	{
		public void BuildFrom(TypeDefinition discovered, ClassDefinition building)
		{
			Debug.Assert(discovered != null, "Type definition is null");
			Debug.Assert(building != null, "Class definition is null");

			if (discovered.HasFields)
			{
				foreach (var field in discovered.Fields.Where(x => x.IsInPublicApi()))
				{
					if (field.IsLiteral)
					{
						building.Constants.Add(BuildConstantFrom(field));
					}
					else
					{
						building.Fields.Add(BuildFieldFrom(field));
					}
				}
			}
		}

		public Constant BuildConstantFrom(FieldDefinition fd)
		{
			Debug.Assert(fd != null, "Field definition is null");
			Debug.Assert(!fd.IsPrivate, "Not documenting private fields");
			Debug.Assert(fd.IsLiteral, "Not a constant");

			var builtConstant = new Constant();

			builtConstant.Name = fd.Name;
			builtConstant.ConstantType = new FullyQualifiedName(fd.FieldType.FriendlyName());

			if (fd.IsPublic)
			{
				builtConstant.Accessibility = MemberAccessibility.Public;
			}
			else if (fd.IsProtected())
			{
				builtConstant.Accessibility = MemberAccessibility.Protected;
			}

			var obsBuilder = new ObsoleteBuilder();
			obsBuilder.BuildFrom(fd, builtConstant);

			return builtConstant;
		}

		public MemberField BuildFieldFrom(FieldDefinition fd)
		{
			Debug.Assert(fd != null, "Field definition is null");
			Debug.Assert(!fd.IsPrivate, "Not documenting private fields");

			var builtField = new MemberField();

			builtField.Name = fd.Name;
			builtField.FieldType = new FullyQualifiedName(fd.FieldType.FriendlyName());

			if (fd.IsPublic)
			{
				builtField.Accessibility = MemberAccessibility.Public;
			}
			else if (fd.IsProtected())
			{
				builtField.Accessibility = MemberAccessibility.Protected;
			}

			var obsBuilder = new ObsoleteBuilder();
			obsBuilder.BuildFrom(fd, builtField);

			return builtField;
		}
	}

}

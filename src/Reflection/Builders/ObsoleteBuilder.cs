using Mono.Cecil;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reflection.Builders
{
	public class ObsoleteBuilder
	{
		public void BuildFrom(ICustomAttributeProvider discovered, IMaybeObsolete building)
		{
			var attr = FindObsoleteAttribute(discovered.CustomAttributes);

			if (attr != null)
			{
				building.ObsoleteMarker = new Obsolete();

				if (attr.HasConstructorArguments)
				{
					building.ObsoleteMarker.Message = attr.ConstructorArguments[0].Value.ToString();
				}
			}
		}

		private CustomAttribute FindObsoleteAttribute(IEnumerable<CustomAttribute> attributes)
		{
			var obsoleteAttr = typeof(ObsoleteAttribute).ToString();

			return attributes.FirstOrDefault(x => x.AttributeType.FullName == obsoleteAttr);
		}
	}
}

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
	public class SignatureBuilder
	{
		public Signature BuildFrom(MethodDefinition md)
		{
			Debug.Assert(md != null, "Method definition is null");

			var signature = new Signature
			{
				Name = md.Name
			};

			if (md.IsSpecialName)
			{
				signature.Name = md.DeclaringType.Name;
			}

			if (md.HasParameters)
			{
				foreach (var parameter in md.Parameters)
				{
					signature.Add(new Parameter(new FullyQualifiedName(parameter.ParameterType.FriendlyName())));
				}
			}

			return signature;
		}
	}

}

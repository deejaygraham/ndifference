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
	public class MethodBuilder
	{
		public bool SuppressAbstractModifier { get; set; }

		public bool SuppressVirtualModifier { get; set; }

		public void BuildFrom(TypeDefinition discovered, IReferenceTypeDefinition building)
		{
			Debug.Assert(discovered != null, "Type definition is null");
			Debug.Assert(building != null, "Class definition is null");

			var obsoleteBuilder = new ObsoleteBuilder();

			if (discovered.HasMethods)
			{
				ClassDefinition potentialClass = building as ClassDefinition;

				foreach (var method in discovered.Methods)
				{
					if (method.IsSpecialName)
					{
						// constructors etc.
						if (method.IsConstructor && potentialClass != null)
						{
							if (method.IsStatic)
							{
								potentialClass.StaticConstructor = BuildStaticConstructorFrom(method);
							}
							else if (method.IsPublic)
							{
								potentialClass.Constructors.Add(BuildConstructorFrom(method));
							}
						}
					}
					else
					{
						if (method.IsFinalizer() && potentialClass != null)
						{
							potentialClass.Finalizer = BuildFinalizerFrom(method);
						}
						else if (method.IsInPublicApi())
						{
							MemberMethod im = BuildInstanceMethodFrom(method);
							building.Methods.Add(im);
						}
					}
				}
			}
		}

		private InstanceConstructor BuildConstructorFrom(MethodDefinition md)
		{
			var ic = new InstanceConstructor
			{
				Accessibility = md.IsProtected()
				? MemberAccessibility.Protected
				: MemberAccessibility.Public
			};

			var sigBuild = new SignatureBuilder();
			ic.Signature = sigBuild.BuildFrom(md);

			var obsBuilder = new ObsoleteBuilder();
			obsBuilder.BuildFrom(md, ic);

			return ic;
		}

		private StaticConstructor BuildStaticConstructorFrom(MethodDefinition md)
		{
			var sc = new StaticConstructor(md.DeclaringType.Name);

			var obsBuilder = new ObsoleteBuilder();
			obsBuilder.BuildFrom(md, sc);

			return sc;
		}

		private Finalizer BuildFinalizerFrom(MethodDefinition md)
		{
			Finalizer fin = new Finalizer(md.DeclaringType.Name);

			var obsBuilder = new ObsoleteBuilder();
			obsBuilder.BuildFrom(md, fin);

			return fin;
		}

		private MemberMethod BuildInstanceMethodFrom(MethodDefinition md)
		{
			MemberMethod im = new MemberMethod();
			im.ReturnType = new FullyQualifiedName(md.ReturnType.FriendlyName());
			im.Accessibility = md.IsProtected()
				? MemberAccessibility.Protected
				: MemberAccessibility.Public;
			im.IsAbstract = this.SuppressAbstractModifier ? false : md.IsAbstract;
			im.IsStatic = md.IsStatic;
			im.IsVirtual = this.SuppressVirtualModifier ? false : md.IsVirtual;

			var sigBuild = new SignatureBuilder();
			im.Signature = sigBuild.BuildFrom(md);

			var obsBuilder = new ObsoleteBuilder();
			obsBuilder.BuildFrom(md, im);

			return im;
		}
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// A method belonging to a class.
	/// </summary>
	public class MethodDeclaration
	{
		// FIXME: Need to handle virtual and abstract
		public Signature Signature { get; set; }

		public FullyQualifiedName ReturnType { get; set; }

		public InstanceAccessModifier Access { get; set; }

		public bool IsInstance
		{
			get
			{
				return this.Access == InstanceAccessModifier.Instance;
			}

			set
			{
				this.Access = value ? InstanceAccessModifier.Instance : InstanceAccessModifier.Static;
			}
		}

		public bool IsStatic
		{
			get
			{
				return this.Access == InstanceAccessModifier.Static;
			}

			set
			{
				this.Access = value ? InstanceAccessModifier.Static : InstanceAccessModifier.Instance;
			}
		}
	}

	// find overloads for a method name
	// overload resolution...
}

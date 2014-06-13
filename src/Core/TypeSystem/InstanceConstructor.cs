using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// Initializes an instance of a class.
	/// </summary>
	[Serializable]
	public class InstanceConstructor : IMemberMethod
	{
		public MemberAccessibility Accessibility { get; set; }

		public Signature Signature { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public bool IsDefault
		{
			get
			{
				return !this.Signature.FormalParameters.Any();
			}
		}

		public void Add(Parameter parameter)
		{
			this.Signature.Add(parameter);
		}

		public override string ToString()
		{
			return this.Signature.ToString();
		}

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			//code.Add(this.TypeName.ToCode());
			//code.Add(new PunctuationTag("("));
			//code.Add(this.Signature.ToCode());
			//code.Add(new PunctuationTag(")"));

			return code;
		}
	}
}

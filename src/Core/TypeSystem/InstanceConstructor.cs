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

		public bool IsAbstract { get; set; }

		public bool IsStatic { get; set; }

		public bool IsVirtual { get { return false; } }

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

			code.Add(this.Signature.ToCode());

			return code;
		}

		public bool ExactlyMatches(IMemberMethod other)
		{
			return string.Compare(
				this.ToString(),
				other.ToString(),
				StringComparison.Ordinal) == 0;
		}

		public bool FuzzyMatches(IMemberMethod other)
		{
			return this.Signature.FuzzyMatches(other.Signature);
		}

	}
}

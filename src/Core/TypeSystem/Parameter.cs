using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// A method parameter.
	/// </summary>
	// value parameter - optional
	// reference parameter
	// output parameter
	// parameter array.
	[DebuggerDisplay("arg {Name.Type.Value}")]
	[Serializable]
	public class Parameter : ISourceCodeProvider
	{
		public Parameter()
		{
		}

		public Parameter(FullyQualifiedName fqn)
		{
			this.Name = fqn;
		}

		public FullyQualifiedName Name { get; set; }

		public override string ToString()
		{
			return this.Name.ToString();
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		public ICoded ToCode()
		{
			return this.Name.ToCode();
		}
	}

}

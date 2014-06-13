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
	/// One "allowed" value in an enum.
	/// </summary>
	[DebuggerDisplay("{Name} = {Value}")]
	[Serializable]
	public class EnumValue : ISourceCodeProvider, IEquatable<EnumValue>
	{
		public EnumValue()
		{
		}

		public EnumValue(string name, long value)
		{
			this.Name = name;
			this.Value = value;
		}

		public string Name { get; set; }

		public long Value { get; set; }

		public override string ToString()
		{
			return string.Format("{0} ({1})", this.Name, this.Value);
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			EnumValue ev = obj as EnumValue;

			if ((object)ev == null)
			{
				return false;
			}

			return this.Name == ev.Name && this.Value == ev.Value;
		}

		public bool Equals(EnumValue other)
		{
			if ((object)other == null)
			{
				return false;
			}

			return this.Name == other.Name && this.Value == other.Value;
		}

		public static bool operator ==(EnumValue a, EnumValue b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}

			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return a.Name == b.Name && a.Value == b.Value;
		}

		public static bool operator !=(EnumValue a, EnumValue b)
		{
			return !(a == b);
		}

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(new IdentifierTag(this.Name));
			code.Add(new PunctuationTag(string.Format(" = {0}", this.Value)));

			return code;
		}
	}
}

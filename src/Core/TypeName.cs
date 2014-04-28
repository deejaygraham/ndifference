using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	/// <summary>
	/// Simple name of a type in an assembly. Does not contain namespace info.
	/// </summary>
	[DebuggerDisplay("type {Value}")]
	public class TypeName : IComparable<TypeName> 
	{
		public TypeName(string value)
		{
			this.Value = value;
		}

		public string Value { get; private set; }

		public static bool operator ==(TypeName a, TypeName b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}

			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return string.Compare(a.Value, b.Value, StringComparison.Ordinal) == 0;
		}

		public static bool operator !=(TypeName a, TypeName b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			TypeName another = obj as TypeName;

			if ((object)another == null)
			{
				return false;
			}

			return string.Compare(this.Value, another.Value, StringComparison.Ordinal) == 0;
		}

		public override string ToString()
		{
			return this.Value;
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		public int CompareTo(TypeName other)
		{
			if (other == null)
			{
				return 1;
			}

			return this.Value.CompareTo(other.Value);
		}
	}
}

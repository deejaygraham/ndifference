using System;
using System.Diagnostics;
using System.Linq;

namespace NDifference
{
	/// <summary>
	/// Full name of an a defined type - namespace & type name
	/// </summary>
	[DebuggerDisplay("type {Value}")]
	public class FullyQualifiedName : IComparable<FullyQualifiedName>
	{
		public FullyQualifiedName(string fqn)
		{
			Debug.Assert(!string.IsNullOrEmpty(fqn), "Name cannot be blank");

			this.Value = fqn;
		}

		public string Value { get; private set; }

		public TypeName Type
		{
			get
			{
				string text = this.Value;

				const char Dot = '.';

				if (this.Value.Contains(Dot))
				{
					int index = this.Value.LastIndexOf(Dot);
					text = this.Value.Substring(index + 1);
				}

				return new TypeName(text);
			}
		}

		public Namespace ContainingNamespace
		{
			get
			{
				string text = string.Empty;

				const char Dot = '.';

				if (this.Value.Contains(Dot))
				{
					int index = this.Value.LastIndexOf(Dot);
					text = this.Value.Substring(0, index);
				}

				return new Namespace(text);
			}
		}

		public static bool operator ==(FullyQualifiedName a, FullyQualifiedName b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}

			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return a.ContainingNamespace == b.ContainingNamespace && a.Type == b.Type;
		}

		public static bool operator !=(FullyQualifiedName a, FullyQualifiedName b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			FullyQualifiedName another = obj as FullyQualifiedName;

			if ((object)another == null)
			{
				return false;
			}

			const bool IgnoreCase = false;

			return string.Compare(this.Value, another.Value, IgnoreCase) == 0;
		}

		public override string ToString()
		{
			return this.Value;
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		public int CompareTo(FullyQualifiedName other)
		{
			if (other == null)
			{
				return 1;
			}

			if (this.ContainingNamespace != other.ContainingNamespace)
			{
				return this.ContainingNamespace.CompareTo(other.ContainingNamespace);
			}

			return this.Value.CompareTo(other.Value);
		}
	}
}

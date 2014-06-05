using System;
using System.Diagnostics;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// An organizational unit within an assembly.
	/// </summary>
	[DebuggerDisplay("namespace {Value}")]
	public class Namespace : IComparable<Namespace>
	{
		public Namespace()
			: this(string.Empty)
		{
		}

		public Namespace(string value)
		{
			this.Value = value;
		}

		public bool IsGlobal 
		{ 
			get 
			{ 
				return string.IsNullOrEmpty(this.Value); 
			} 
		}

		public bool IsSystem 
		{ 
			get 
			{
				const string SystemNamespace = "System";
				return this.Value == SystemNamespace; 
			} 
		}

		public string Value { get; private set; }

		public static FullyQualifiedName operator +(Namespace ns, TypeName tn)
		{
			Debug.Assert(ns != null, "NamespaceName is blank");
			Debug.Assert(tn != null, "TypeName is blank");
			Debug.Assert(!string.IsNullOrEmpty(tn.Value), "TypeName is blank");

			if (string.IsNullOrEmpty(ns.Value))
				return new FullyQualifiedName(tn.Value);

			return new FullyQualifiedName(string.Format("{0}.{1}", ns.Value, tn.Value));
		}

		public static bool operator ==(Namespace a, Namespace b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}

			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return string.Compare(a.Value, b.Value, StringComparison.OrdinalIgnoreCase) == 0;
		}

		public static bool operator !=(Namespace a, Namespace b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Namespace another = obj as Namespace;

			if ((object)another == null)
			{
				return false;
			}

			return string.Compare(this.Value, another.Value, StringComparison.OrdinalIgnoreCase) == 0;
		}

		public override string ToString()
		{
			return this.Value;
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		public int CompareTo(Namespace other)
		{
			if (other == null)
			{
				return 1;
			}

			return this.Value.CompareTo(other.Value);
		}
	}
}

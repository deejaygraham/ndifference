
using NDifference.SourceFormatting;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace NDifference.TypeSystem
{
    /// <summary>
	/// Plain old C# Object. No distinction between enum, class, 
	/// struct, interface needed yet.
	/// </summary>
	[Serializable]
	[DebuggerDisplay("{Taxonomy} {Name}")]
	public class PocoType : ITypeInfo
	{
		[NonSerialized]
		private Identifier ident = new Identifier();
				
		public string Identifier
		{
			get
			{
				return this.ident;
			}

			set
			{
				this.ident = value;
			}
		}

		public TypeTaxonomy Taxonomy { get; set; }

		public AccessModifier Access { get; set; }

		public string Namespace { get; set; }

		public string Name { get; set; }

		public string FullName { get; set; }

		public string Assembly { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public override string ToString()
		{
			return this.FullName.ToString();
		}

		public override int GetHashCode()
		{
			return this.FullName.GetHashCode();
		}

		public string CalculateHash()
		{
			return this.GetHash(SHA256.Create());
		}

		public static bool operator ==(PocoType leftHandSide, PocoType rightHandSide)
		{
			if (object.ReferenceEquals(leftHandSide, rightHandSide))
			{
				return true;
			}

			if (((object)leftHandSide == null) || ((object)rightHandSide == null))
			{
				return false;
			}

			return leftHandSide.FullName == rightHandSide.FullName;
		}

		public static bool operator !=(PocoType leftHandSide, PocoType rightHandSide)
		{
			return !(leftHandSide == rightHandSide);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			PocoType otherType = obj as PocoType;

			if ((object)otherType == null)
			{
				return false;
			}

			return this.FullName == otherType.FullName;
		}

		public bool Equals(ITypeInfo other)
		{
			if ((object)other == null)
			{
				return false;
			}

			return this.FullName == other.FullName;
		}

		public ICoded ToCode ()
		{
			return new SourceCode();
		}
	}
}

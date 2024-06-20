using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	[DebuggerDisplay("enum {FullName}")]
	[Serializable]
	public class EnumDefinition : ITypeInfo
	{
		[NonSerialized]
		private Identifier ident = new Identifier();

		[NonSerialized]
		private string assemblyName;

		[NonSerialized]
		private string _hashValue;

		public EnumDefinition()
		{
			this.AllowedValues = new List<EnumValue>();
		}

		public EnumDefinition(string name)
			: this()
		{
			this.FullName = name;
		}

		public List<EnumValue> AllowedValues { get; private set; }

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

		public TypeTaxonomy Taxonomy { get { return TypeTaxonomy.Enum; } }

		public AccessModifier Access { get; set; }

		public string Namespace { get; set; }

		public string Name { get; set; }

		public string FullName { get; set; }

		public string Assembly { get { return assemblyName; } set { assemblyName = value; } }

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
			if (String.IsNullOrEmpty(this._hashValue))
				this._hashValue = this.GetHash(SHA256.Create());

			return this._hashValue;
		}

		public void Add(string name, long value)
		{
			this.AllowedValues.Add(new EnumValue(name, value));
		}

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(new IdentifierTag(this.Name));

			return code;

		}

		public static bool operator ==(EnumDefinition a, EnumDefinition b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}

			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return a.FullName == b.FullName;
		}

		public static bool operator !=(EnumDefinition a, EnumDefinition b)
		{
			return !(a == b);
		}
		
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			EnumDefinition et = obj as EnumDefinition;

			if ((object)et == null)
			{
				return false;
			}

			return this.FullName == et.FullName;
		}
	
		public bool Equals(ITypeInfo other)
		{
			if ((object)other == null)
			{
				return false;
			}

			return this.FullName == other.FullName;
		}
	}
}

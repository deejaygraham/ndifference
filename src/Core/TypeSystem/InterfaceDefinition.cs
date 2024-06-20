using NDifference.Inspection;
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
	/// <summary>
	/// Represents an interface "contract".
	/// </summary>
	[DebuggerDisplay("interface {FullName}")]
	[Serializable]
	public class InterfaceDefinition : ITypeInfo, ISourceCodeProvider, IReferenceTypeDefinition
	{
		[NonSerialized]
		private Identifier ident = new Identifier();

		[NonSerialized]
		private string assemblyName;

		[NonSerialized]
		private string _hashValue;
		
		public InterfaceDefinition()
		{
			this.AllMethods = new List<IMemberMethod>();
			this.AllProperties = new List<MemberProperty>();
			this.Events = new List<MemberEvent>();
			this.Indexers = new List<Indexer>();
			this.Implements = new List<FullyQualifiedName>();
		}

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

		public TypeTaxonomy Taxonomy { get { return TypeTaxonomy.Interface; } }

		public AccessModifier Access { get; set; }

		public string Namespace { get; set; }

		public string Name { get; set; }

		public string FullName { get; set; }

		public string Assembly { get { return assemblyName; } set { assemblyName = value; } }

		public Obsolete ObsoleteMarker { get; set; }

		public List<IMemberMethod> AllMethods { get; set; }

		public List<IMemberMethod> Methods(MemberVisibilityOption accessibility)
		{
			return this.AllMethods;
		}

		public List<MemberProperty> AllProperties { get; set; }

		public List<MemberProperty> Properties(MemberVisibilityOption accessibility)
		{
			return this.AllProperties;
		}

		public List<MemberEvent> Events { get; set; }

		public List<Indexer> Indexers { get; set; }
		
		/// <summary>
		/// Which interfaces does this interface inherit?
		/// </summary>
		public List<FullyQualifiedName> Implements { get; set; }

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

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(new IdentifierTag(this.FullName));

			return code;
		}

		public static bool operator ==(InterfaceDefinition a, InterfaceDefinition b)
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

		public static bool operator !=(InterfaceDefinition a, InterfaceDefinition b)
		{
			return !(a == b);
		}
		
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			InterfaceDefinition et = obj as InterfaceDefinition;

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

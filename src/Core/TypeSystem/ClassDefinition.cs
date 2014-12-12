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
	[DebuggerDisplay("class {FullName}")]
	[Serializable]
	public class ClassDefinition : ITypeInfo, ISourceCodeProvider, IReferenceTypeDefinition
	{
		[NonSerialized]
		private Identifier ident = new Identifier();

		[NonSerialized]
		private string assemblyName;

		[NonSerialized]
		private string _hashValue;

		public ClassDefinition()
		{
			this.Implements = new List<FullyQualifiedName>();
			this.Constants = new List<Constant>();
			this.Fields = new List<MemberField>();
			this.AllMethods = new List<IMemberMethod>();
			this.AllProperties = new List<MemberProperty>();
			this.Events = new List<MemberEvent>();
			this.Indexers = new List<Indexer>();
			this.Operators = new List<Operator>();
			this.Constructors = new List<InstanceConstructor>();
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

		public TypeTaxonomy Taxonomy { get { return TypeTaxonomy.Class; } }

		public AccessModifier Access { get; set; }

		public string Namespace { get; set; }

		public string Name { get; set; }

		public string FullName { get; set; }

		public string Assembly { get { return assemblyName; } set { assemblyName = value; } }

		public Obsolete ObsoleteMarker { get; set; }

		/// <summary>
		/// Is this class explicitly sealed?
		/// </summary>
		public bool IsSealed { get; set; }

		/// <summary>
		/// Is this class abstract?
		/// </summary>
		public bool IsAbstract { get; set; }

		public bool IsSubclass
		{
			get
			{
				return this.InheritsFrom != null;
			}
		}

		/// <summary>
		/// Which class does this class derive from (if any)?
		/// </summary>
		public FullyQualifiedName InheritsFrom { get; set; }

		/// <summary>
		/// Which interfaces does this class implement?
		/// </summary>
		public List<FullyQualifiedName> Implements { get; set; }

		public List<Constant> Constants { get; set; }

		public List<MemberField> Fields { get; set; }

		public List<IMemberMethod> AllMethods { get; set; }

		public List<IMemberMethod> Methods(MemberVisibilityOption accessibility)
		{
			if (accessibility == MemberVisibilityOption.All)
				return this.AllMethods;

			return this.AllMethods.Where(x => x.Accessibility == MemberAccessibility.Public).ToList();
		}

		public List<MemberProperty> AllProperties { get; set; }

		public List<MemberProperty> Properties(MemberVisibilityOption accessibility)
		{
			if (accessibility == MemberVisibilityOption.All)
				return this.AllProperties;

			return this.AllProperties.Where(x => x.Accessibility == MemberAccessibility.Public).ToList();
		}

		public List<MemberEvent> Events { get; set; }

		public List<Indexer> Indexers { get; set; }

		public List<Operator> Operators { get; set; }

		public List<InstanceConstructor> Constructors { get; set; }

		public StaticConstructor StaticConstructor { get; set; }

		public Finalizer Finalizer { get; set; }

		// embedded types

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
				this._hashValue = this.GetHash<SHA1Managed>();

			return this._hashValue;
		}

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(new KeywordTag("public"));

			if (this.IsAbstract)
			{
				code.Add(new KeywordTag("abstract"));
			}

			if (this.IsSealed)
			{
				code.Add(new KeywordTag("sealed"));
			}

			code.Add(new KeywordTag("class"));
			code.Add(new TypeNameTag(this.Name));

			if (this.IsSubclass || this.Implements.Count > 0)
			{
				code.Add(new PunctuationTag(":"));
			}

			if (this.IsSubclass)
			{
				code.Add(new TypeNameTag(this.InheritsFrom.Type.Value));
			}

			if (this.IsSubclass && this.Implements.Count > 0)
			{
				code.Add(new PunctuationTag(","));
			}

			if (this.Implements.Count > 0)
			{
				for (int i = 0; i < this.Implements.Count; ++i)
				{
					var interf = this.Implements[i].Type;

					code.Add(new TypeNameTag(interf.Value));

					if (i < this.Implements.Count - 1)
					{
						code.Add(new PunctuationTag(","));
					}
				}
			}

			return code;
		}

		public static bool operator ==(ClassDefinition a, ClassDefinition b)
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

		public static bool operator !=(ClassDefinition a, ClassDefinition b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			ClassDefinition et = obj as ClassDefinition;

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

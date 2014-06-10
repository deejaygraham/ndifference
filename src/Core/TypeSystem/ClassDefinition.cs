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
	public class ClassDefinition : ITypeInfo, ISourceCodeProvider
	{
		[NonSerialized]
		private Identifier ident = new Identifier();

		public ClassDefinition()
		{
			//this.Implements = new List<FullyQualifiedName>();
			//this.Constants = new List<Constant>();
			//this.Fields = new List<Field>();
			//this.Methods = new List<IMethod>();
			//this.Properties = new List<Property>();
			//this.Events = new List<Event>();
			//this.Indexers = new List<Indexer>();
			//this.Operators = new List<Operator>();
			//this.Constructors = new List<InstanceConstructor>();
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

		public string Assembly { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		/// <summary>
		/// Is this class explicitly sealed?
		/// </summary>
		public bool IsSealed { get; set; }

		/// <summary>
		/// Is this class abstract?
		/// </summary>
		public bool IsAbstract { get; set; }

		//public bool IsSubclass
		//{
		//	get
		//	{
		//		return this.InheritsFrom != null;
		//	}
		//}

		///// <summary>
		///// Which class does this class derive from (if any)?
		///// </summary>
		//public FullyQualifiedName InheritsFrom { get; set; }

		///// <summary>
		///// Which interfaces does this class implement?
		///// </summary>
		//public List<FullyQualifiedName> Implements { get; set; }

		//public List<Constant> Constants { get; set; }

		//public List<Field> Fields { get; set; }

		//public List<IMethod> Methods { get; set; }

		//public List<Property> Properties { get; set; }

		//public List<Event> Events { get; set; }

		//public List<Indexer> Indexers { get; set; }

		//public List<Operator> Operators { get; set; }

		//public List<InstanceConstructor> Constructors { get; set; }

		//public StaticConstructor StaticConstructor { get; set; }

		//public Finalizer Finalizer { get; set; }

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
			return this.GetHash<SHA1Managed>();
		}

		public SourceCode ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(new IdentifierTag(this.FullName));

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


namespace NDifference
{
    /// <summary>
	/// Plain old C# Object. No distinction between enum, class, 
	/// struct, interface needed yet.
	/// </summary>
	public class PocoType : ITypeInfo
	{
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

		//// public Obsolete ObsoleteMarker { get; set; }

		public override string ToString()
		{
			return this.FullName.ToString();
		}

		public override int GetHashCode()
		{
			return this.FullName.GetHashCode();
		}
	}
}

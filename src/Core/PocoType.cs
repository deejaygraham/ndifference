
namespace NDifference
{
    /// <summary>
	/// Plain old C# Object. No distinction between enum, class, 
	/// struct, interface needed yet.
	/// </summary>
	public class PocoType : IDiscoveredType
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

		public FullyQualifiedName FullName { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

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

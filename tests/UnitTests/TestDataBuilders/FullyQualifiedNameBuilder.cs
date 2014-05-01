
namespace NDifference.UnitTests
{
	public class FullyQualifiedNameBuilder : IBuildable<FullyQualifiedName>
	{
		private string Name { get; set; }

		private string NamespaceName { get; set; }

		public static FullyQualifiedNameBuilder Type()
		{
			return new FullyQualifiedNameBuilder();
		}

		public FullyQualifiedNameBuilder Named(string name)
		{
			this.Name = name;
			return this;
		}

		public FullyQualifiedNameBuilder InNamespace(string ns)
		{
			this.NamespaceName = ns;
			return this;
		}

		public FullyQualifiedName Build()
		{
			return new Namespace(this.NamespaceName) + new TypeName(this.Name);
		}
	}
}

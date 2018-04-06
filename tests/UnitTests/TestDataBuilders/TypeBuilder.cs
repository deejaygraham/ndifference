using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.UnitTests.TestDataBuilders
{
    public class TypeBuilder : IBuildable<ITypeInfo>
    {
        private string Name { get; set; }

        private string NamespaceName { get; set; }

        private TypeTaxonomy Taxonomy { get; set; }

        private AccessModifier Access { get; set; }

        public static TypeBuilder Class()
        {
            return new TypeBuilder
            {
                Taxonomy = TypeTaxonomy.Class,
                Access = AccessModifier.Public
            };
        }

        public static TypeBuilder Enum()
        {
            return new TypeBuilder
            {
                Taxonomy = TypeTaxonomy.Enum,
                Access = AccessModifier.Public
            };
        }

        public TypeBuilder Named(string name)
        {
            this.Name = name;
            return this;
        }

        public TypeBuilder InNamespace(string ns)
        {
            this.NamespaceName = ns;
            return this;
        }

        public TypeBuilder IsInternal()
        {
            this.Access = AccessModifier.Internal;
            return this;
        }

        public ITypeInfo Build()
        {
            var poco = new PocoType
            {
                Access = this.Access,
                Taxonomy = this.Taxonomy,
                Namespace = this.NamespaceName,
                Name = this.Name,
            };

            if (String.IsNullOrEmpty(this.NamespaceName))
                poco.FullName = this.Name;
            else
                poco.FullName = string.Format("{0}.{1}", this.NamespaceName, this.Name);

            return poco;
        }
    }
}

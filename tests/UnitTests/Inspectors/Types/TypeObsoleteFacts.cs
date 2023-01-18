using NDifference.Inspectors;
using Xunit;

namespace NDifference.UnitTests.Inspectors.Types
{
    public class TypeObsoleteFacts
    {
        [Fact]
        public void ObsoleteTypeInspector_Identifies_When_Class_Is_Made_Obsolete()
        {
            var oldClassBuilder = CompilableClassBuilder.PublicClass()
                .Named("Account");

            var newClassBuilder = CompilableClassBuilder.PublicClass()
                .Named("Account")
                .IsObsolete();

            var delta = IdentifiedChangeCollectionBuilder.Changes()
                .From(oldClassBuilder)
                .To(newClassBuilder)
                .InspectedBy(new ObsoleteTypeInspector())
                .Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
        public void ObsoleteTypeInspector_Reports_When_Class_Is_Always_Obsolete()
        {
            var oldClassBuilder = CompilableClassBuilder.PublicClass()
                .Named("Account")
                .IsObsolete();

            var newClassBuilder = CompilableClassBuilder.PublicClass()
                .Named("Account")
                .IsObsolete();

            var delta = IdentifiedChangeCollectionBuilder.Changes()
                .From(oldClassBuilder)
                .To(newClassBuilder)
                .InspectedBy(new ObsoleteTypeInspector())
                .Build();

            Assert.Single(delta.Changes);
        }
    }
}

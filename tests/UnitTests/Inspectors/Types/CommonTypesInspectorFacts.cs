using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Inspectors;
using NDifference.TypeSystem;
using NDifference.UnitTests.TestDataBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class CommonTypesInspectorFacts
	{
		[Fact]
		public void CommonTypesInspector_Ignores_Identical_Lists()
		{
			var first = new List<ITypeInfo>();
            first.Add(TypeBuilder.Class().Named("First").InNamespace("Example").Build());
            first.Add(TypeBuilder.Class().Named("Second").InNamespace("Example").Build());
            first.Add(TypeBuilder.Class().Named("Third").InNamespace("Example").Build());

			var second = new List<ITypeInfo>();
            second.Add(TypeBuilder.Class().Named("First").InNamespace("Example").Build());
            second.Add(TypeBuilder.Class().Named("Second").InNamespace("Example").Build());
            second.Add(TypeBuilder.Class().Named("Third").InNamespace("Example").Build());

            ITypeCollectionInspector inspector = new CommonTypesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedObjectModel.BuildFrom(first, second), changes);

			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.AddedTypes));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.RemovedTypes));
            Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.PotentiallyChangedTypes));
        }

        [Fact]
		public void CommonTypesInspector_Identifies_Change_From_Class_To_Enum()
		{
			var first = new List<ITypeInfo>();

            first.Add(TypeBuilder.Class().Named("First").InNamespace("Example").Build());
            first.Add(TypeBuilder.Class().Named("Second").InNamespace("Example").Build());
            first.Add(TypeBuilder.Class().Named("Third").InNamespace("Example").Build());

			var second = new List<ITypeInfo>();
            second.Add(TypeBuilder.Class().Named("First").InNamespace("Example").Build());
            second.Add(TypeBuilder.Class().Named("Second").InNamespace("Example").Build());
            second.Add(TypeBuilder.Enum().Named("Third").InNamespace("Example").Build());

			ITypeCollectionInspector inspector = new CommonTypesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedObjectModel.BuildFrom(first, second), changes);

			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.AddedTypes));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.RemovedTypes));
			Assert.Single(changes.ChangesInCategory(WellKnownChangePriorities.PotentiallyChangedTypes));
		}

	}

}

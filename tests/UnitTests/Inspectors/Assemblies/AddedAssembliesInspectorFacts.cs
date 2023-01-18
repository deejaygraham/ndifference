using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Inspectors;
using System.Collections.Generic;
using Xunit;

namespace NDifference.UnitTests.Inspectors.Assemblies
{
	public class AddedAssembliesInspectorFacts
	{
		[Fact]
		public void AddedAssembliesInspector_Ignores_Identical_Lists()
		{
			var first = new List<IAssemblyDiskInfo>();
			first.Add(new AssemblyDiskInfo { Name = "First.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			var second = new List<IAssemblyDiskInfo>();
			second.Add(new AssemblyDiskInfo { Name = "First.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			IAssemblyCollectionInspector inspector = new AddedAssembliesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedAssemblyModel.BuildFrom(first, second), changes);

			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.AddedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.RemovedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies));
		}					

		[Fact]
		public void AddedAssembliesInspector_Identifies_Added_Assemblies()
		{
			var first = new List<IAssemblyDiskInfo>();
			first.Add(new AssemblyDiskInfo { Name = "First.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			var second = new List<IAssemblyDiskInfo>();
			second.Add(new AssemblyDiskInfo { Name = "Fourth.dll" });
			second.Add(new AssemblyDiskInfo { Name = "First.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Fifth.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			IAssemblyCollectionInspector inspector = new AddedAssembliesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedAssemblyModel.BuildFrom(first, second), changes);

			Assert.Equal(2, changes.ChangesInCategory(WellKnownChangePriorities.AddedAssemblies).Count);
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.RemovedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies));
		}

	}
}

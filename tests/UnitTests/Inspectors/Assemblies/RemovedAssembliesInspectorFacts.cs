using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Inspectors;
using System.Collections.Generic;
using Xunit;

namespace NDifference.UnitTests.Inspectors.Assemblies
{
	public class RemovedAssembliesInspectorFacts
	{
		[Fact]
		public void RemovedAssembliesInspector_Ignores_Identical_Lists()
		{
			var first = new List<IAssemblyDiskInfo>();
			first.Add(new AssemblyDiskInfo { Name = "First.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			var second = new List<IAssemblyDiskInfo>();
			second.Add(new AssemblyDiskInfo { Name = "First.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			IAssemblyCollectionInspector inspector = new RemovedAssembliesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedAssemblyModel.BuildFrom(first, second), changes);

			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.AddedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.RemovedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies));
		}

		[Fact]
		public void RemovedAssembliesInspector_Identifies_Removed_Assemblies()
		{
			var first = new List<IAssemblyDiskInfo>();
			first.Add(new AssemblyDiskInfo { Name = "First.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Third.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Fourth.dll" });

			var second = new List<IAssemblyDiskInfo>();
			second.Add(new AssemblyDiskInfo { Name = "First.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			IAssemblyCollectionInspector inspector = new RemovedAssembliesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedAssemblyModel.BuildFrom(first, second), changes);

			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.AddedAssemblies));
			Assert.Single(changes.ChangesInCategory(WellKnownChangePriorities.RemovedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies));
		}

	}

}

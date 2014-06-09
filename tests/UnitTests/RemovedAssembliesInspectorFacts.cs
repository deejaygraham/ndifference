using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Inspectors;
using System.Collections.Generic;
using Xunit;

namespace NDifference.UnitTests
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

			inspector.Inspect(first, second, changes);

			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.AddedAssemblies).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.RemovedAssemblies).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies).Count);
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

			inspector.Inspect(first, second, changes);

			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.AddedAssemblies).Count);
			Assert.Equal(1, changes.ChangesInCategory(WellKnownChangePriorities.RemovedAssemblies).Count);
			Assert.Equal(0, changes.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies).Count);
		}

	}

}

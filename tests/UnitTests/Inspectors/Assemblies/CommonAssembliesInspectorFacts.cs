using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using Xunit;

namespace NDifference.UnitTests
{
	public class CommonAssembliesInspectorFacts
	{
		[Fact]
		public void CommonAssembliesInspector_Ignores_Added_Assemblies()
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

			IAssemblyCollectionInspector inspector = new CommonAssembliesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedAssemblyModel.BuildFrom(first, second), changes);

			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.AddedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.RemovedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies));
		}

		[Fact]
		public void CommonAssembliesInspector_Ignores_Removed_Assemblies()
		{
			var first = new List<IAssemblyDiskInfo>();
			first.Add(new AssemblyDiskInfo { Name = "First.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Third.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Fourth.dll" });
			first.Add(new AssemblyDiskInfo { Name = "Fifth.dll" });

			var second = new List<IAssemblyDiskInfo>();
			second.Add(new AssemblyDiskInfo { Name = "First.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Second.dll" });
			second.Add(new AssemblyDiskInfo { Name = "Third.dll" });

			IAssemblyCollectionInspector inspector = new CommonAssembliesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedAssemblyModel.BuildFrom(first, second), changes);

			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.AddedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.RemovedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.ChangedAssemblies));
		}

		[Fact]
		public void CommonAssembliesInspector_Identifies_Common_Assemblies_With_Differing_Details()
		{
			var first = new List<IAssemblyDiskInfo>();
			first.Add(new AssemblyDiskInfo("First.dll", new DateTime(2014, 06, 03), 100, "abcd"));
			first.Add(new AssemblyDiskInfo("Second.dll", new DateTime(2014, 06, 03), 200, "efgh"));
			first.Add(new AssemblyDiskInfo("Third.dll", new DateTime(2014, 06, 03), 300, "ijkl"));

			var second = new List<IAssemblyDiskInfo>();
			second.Add(new AssemblyDiskInfo("First.dll", new DateTime(2014, 06, 06), 100, "abcde"));
			second.Add(new AssemblyDiskInfo("Second.dll", new DateTime(2014, 06, 03), 200, "efgh"));
			second.Add(new AssemblyDiskInfo("Third.dll", new DateTime(2014, 06, 07), 400, "iikl"));

			IAssemblyCollectionInspector inspector = new CommonAssembliesInspector();

			var changes = new IdentifiedChangeCollection();

			inspector.Inspect(CombinedAssemblyModel.BuildFrom(first, second), changes);

			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.AddedAssemblies));
			Assert.Empty(changes.ChangesInCategory(WellKnownChangePriorities.RemovedAssemblies));
		}
	}
}

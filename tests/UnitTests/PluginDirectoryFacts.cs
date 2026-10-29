using NDifference.Inspectors;
using NDifference.Plugins;
using System;
using System.IO;
using Xunit;

namespace NDifference.UnitTests
{
	public class PluginDirectoryFacts
	{
		[Fact]
		public void Current_User_Plugin_Directory_Is_Under_Local_Application_Data()
		{
			string expected = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				"NDifference",
				"Plugins");

			PluginDirectory directory = PluginDirectory.ForCurrentUser();

			Assert.Equal(Path.GetFullPath(expected), directory.Path);
		}

		[Fact]
		public void CreateFinder_Creates_Missing_Plugin_Directory()
		{
			string path = Path.Combine(
				Path.GetTempPath(),
				"NDifference.PluginDirectory",
				Guid.NewGuid().ToString("N"));

			try
			{
				var directory = new PluginDirectory(path);

				IFileFinder finder = directory.CreateFinder();

				Assert.True(Directory.Exists(path));
				Assert.Empty(finder.Find());
			}
			finally
			{
				if (Directory.Exists(path))
				{
					Directory.Delete(path, true);
				}
			}
		}

		[Fact]
		public void ExplicitFinder_Loads_Only_The_Known_BuiltIn_Assembly()
		{
			var finder = new ExplicitFileFinder(
				typeof(RuntimeInspector).Assembly.Location,
				typeof(PluginDirectoryFacts).Assembly.Location);
			finder.Filter = "NDifference.Inspectors.dll";

			IFile file = Assert.Single(finder.Find());

			Assert.Equal("NDifference.Inspectors.dll", file.Name);
		}

		[Fact]
		public void BuiltIn_Inspector_Assembly_Can_Be_Discovered_Explicitly()
		{
			var discoverer = new PluginDiscoverer<IInspector>(
				new ExplicitFileFinder(typeof(RuntimeInspector).Assembly.Location));

			var inspectors = discoverer.Find();

			Assert.NotEmpty(inspectors);
			Assert.All(
				inspectors,
				inspector => Assert.Equal(
					"NDifference.Inspectors",
					inspector.GetType().Assembly.GetName().Name));
		}
	}
}

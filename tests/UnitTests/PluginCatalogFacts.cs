using NDifference.Inspectors;
using NDifference.Plugins;
using NDifference.Reporting;
using System;
using System.Linq;
using System.Runtime.Loader;
using Xunit;

namespace NDifference.UnitTests
{
	public class PluginCatalogFacts
	{
		[Fact]
		public void Catalog_Loads_BuiltIns_Once_And_Reuses_Instances()
		{
			using var catalog = CreateBuiltInCatalog();

			Assert.NotEmpty(catalog.Inspectors);
			Assert.NotEmpty(catalog.ReportWriters);

			InspectorRepository first = catalog.CreateInspectorRepository();
			InspectorRepository second = catalog.CreateInspectorRepository();

			Assert.Same(
				first.TypeInspectors.First(),
				second.TypeInspectors.First());
		}

		[Fact]
		public void Catalog_Loads_Plugins_Into_An_Isolated_Process_Lifetime_Context()
		{
			using var catalog = CreateBuiltInCatalog();

			AssemblyLoadContext loadContext = AssemblyLoadContext.GetLoadContext(
				catalog.Inspectors.First().GetType().Assembly);

			Assert.NotNull(loadContext);
			Assert.NotSame(AssemblyLoadContext.Default, loadContext);
			Assert.False(loadContext.IsCollectible);
		}

		[Fact]
		public void Repository_Creation_Resets_Inspector_State_Before_Filtering()
		{
			using var catalog = CreateBuiltInCatalog();
			ITypeInspector inspector = catalog.Inspectors.OfType<ITypeInspector>().First();

			InspectorRepository first = catalog.CreateInspectorRepository();
			first.Filter(new InspectorFilter(inspector.ShortCode));
			Assert.False(inspector.Enabled);

			catalog.CreateInspectorRepository();

			Assert.True(inspector.Enabled);
		}

		[Fact]
		public void Disposed_Catalog_Rejects_Further_Use()
		{
			PluginCatalog catalog = CreateBuiltInCatalog();

			catalog.Dispose();

			Assert.Throws<ObjectDisposedException>(
				() => catalog.CreateInspectorRepository());
			Assert.Throws<ObjectDisposedException>(
				() => catalog.CreateReportingRepository());
		}

		private static PluginCatalog CreateBuiltInCatalog()
		{
			return new PluginCatalog(
				new ExplicitFileFinder(typeof(RuntimeInspector).Assembly.Location),
				new ExplicitFileFinder(typeof(HtmlReportWriter).Assembly.Location));
		}
	}
}

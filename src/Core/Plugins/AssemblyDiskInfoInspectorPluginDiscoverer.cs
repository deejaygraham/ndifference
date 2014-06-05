using NDifference.Inspectors;

namespace NDifference.Plugins
{
	public class AssemblyDiskInfoInspectorPluginDiscoverer : PluginDiscoverer<IAssemblyCollectionInspector>
	{
		public AssemblyDiskInfoInspectorPluginDiscoverer(IFileFinder finder)
			: base(finder)
		{
		}
	}
}

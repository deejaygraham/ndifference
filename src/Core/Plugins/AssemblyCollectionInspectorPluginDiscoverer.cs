using NDifference.Inspectors;

namespace NDifference.Plugins
{
	public class AssemblyCollectionInspectorPluginDiscoverer : PluginDiscoverer<IAssemblyCollectionInspector>
	{
		public AssemblyCollectionInspectorPluginDiscoverer(IFileFinder finder)
			: base(finder)
		{
		}
	}
}

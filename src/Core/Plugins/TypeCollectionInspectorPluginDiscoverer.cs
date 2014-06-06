using NDifference.Inspectors;

namespace NDifference.Plugins
{
	public class TypeCollectionInspectorPluginDiscoverer : PluginDiscoverer<ITypeCollectionInspector>
	{
		public TypeCollectionInspectorPluginDiscoverer(IFileFinder finder)
			: base(finder)
		{
		}
	}
}

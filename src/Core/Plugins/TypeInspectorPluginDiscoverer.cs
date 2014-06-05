using NDifference.Inspectors;

namespace NDifference.Plugins
{
	public sealed class TypeInspectorPluginDiscoverer : PluginDiscoverer<ITypeInspector>
	{
		public TypeInspectorPluginDiscoverer(IFileFinder finder)
			: base(finder)
		{
		}

	}
}

namespace NDifference.Plugins
{
	public sealed class AssemblyInspectorPluginDiscoverer : PluginDiscoverer<IAssemblyInspector>
	{
		public AssemblyInspectorPluginDiscoverer(IFileFinder finder)
			: base(finder)
		{
		}
	}
}

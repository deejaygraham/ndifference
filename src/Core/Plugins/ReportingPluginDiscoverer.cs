using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Plugins
{
	public sealed class ReportingPluginDiscoverer : PluginDiscoverer<IReportWriter>
	{
		public ReportingPluginDiscoverer(IFileFinder finder)
			: base(finder)
		{
		}
	}
}

using NDifference.Analysis;
using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Checks for changes to target runtime.
	/// </summary>
	public class RuntimeInspector : IAssemblyInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "AI_NETRI"; } }

		public string DisplayName { get { return ".Net Runtime"; } }

		public string Description { get { return "Checks for changes to target runtime"; } }

		public void Inspect(IAssemblyInfo first, IAssemblyInfo second, IdentifiedChangeCollection changes)
		{
			changes.Add(WellKnownAssemblyCategories.AssemblyInternal);

            if (first.RuntimeVersion != second.RuntimeVersion)
			{
				changes.Add(new IdentifiedChange(this, WellKnownAssemblyCategories.AssemblyInternal, 
					new DeltaDescriptor 
					{ 
						Name = ".Net Runtime", 
						Was = first.RuntimeVersion, 
						IsNow = second.RuntimeVersion 
					}));
			}
		}
	}

}

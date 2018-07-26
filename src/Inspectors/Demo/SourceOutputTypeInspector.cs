using NDifference.Analysis;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class SourceOutputTypeInspector : ITypeInspector
	{
		public bool Enabled { get { return false; } set { } }

		public string ShortCode { get { return "TI00SEC"; } }

		public string DisplayName { get { return "Source Writer"; } }

		public string Description { get { return "Writes source code for each type found"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.TypeDebug, new DeltaDescriptor { Was = first.ToCode(), IsNow = second.ToCode() }));
		}
	}
}

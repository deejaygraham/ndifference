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
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI00SEC"; } }

		public string DisplayName { get { return "Source Writer"; } }

		public string Description { get { return "Writes source code for each type found"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			changes.Add(new IdentifiedChange
			{
				Description = "This shows source code",
				Priority = 1,// need value... for type taxonomy-like changes,
				Inspector = this.ShortCode,
				Descriptor = new DeltaDescriptor { Name = "Code", Was = first.ToCode(), IsNow = second.ToCode() }
			});
		}
	}
}

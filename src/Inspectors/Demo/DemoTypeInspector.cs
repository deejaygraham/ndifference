﻿using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;

namespace NDifference.Inspectors
{
// exclude from discovery process
    public class DemoTypeInspector // : ITypeInspector
	{
 // always disabled. Enable here for testing.
		public bool Enabled { get { return false; } set { } }

		public string ShortCode { get { return "TI00DEMO"; } }

		public string DisplayName { get { return "Demo"; } }

		public string Description { get { return "Demonstration used for getting report writing correct."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
        {
            var dummyChange = new IdentifiedChange(WellKnownChangePriorities.TypeDebug,
				Severity.NonBreaking,
				new NameDescriptor
				{
					Reason = "This is a demonstration type inspector-identified change",
					Name = "Demo"
				});

			dummyChange.ForType(first);

            changes.Add(dummyChange);
        }
	}
}

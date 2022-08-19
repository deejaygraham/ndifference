﻿using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
	public class ConstantsAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CAI"; } }

		public string DisplayName { get { return "New Constants"; } }

		public string Description { get { return "Checks for new constants added in the new version"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;
		
			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (secondClass.Constants.Any())
			{
				var added = secondClass.Constants.FindAddedMembers(firstClass.Constants);

				foreach (var add in added)
                {
                    var constantAdded = new IdentifiedChange(WellKnownChangePriorities.ConstantsAdded, new CodeDescriptor { Code = add.ToCode() });

					constantAdded.ForType(first);

                    changes.Add(constantAdded);
                }
			}
		}
	}
}

using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Diagnostics;

namespace NDifference.Inspectors
{
    public class ClassDerivationInspector : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CDI"; } }

		public string DisplayName { get { return "Class Derivation"; } }

		public string Description { get { return "Checks for classes where class hierarchy has changed"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (firstClass.IsSubclass)
			{
				if (secondClass.IsSubclass)
				{
					// check for heirarchy change
					if (firstClass.InheritsFrom != secondClass.InheritsFrom)
                    {
                        var classDerivationChanged = new IdentifiedChange(WellKnownChangePriorities.TypeInternal,
							Severity.BreakingChange,
							new CodeDeltaDescriptor
							{
								Reason = String.Format("Class was derived from {0}, now derived from {1}", firstClass.InheritsFrom, secondClass.InheritsFrom),
								Was = first.ToCode(),
								IsNow = second.ToCode()
							});

						classDerivationChanged.ForType(first);

                        changes.Add(classDerivationChanged);
                    }
				}
				else
                {
                    // no longer derived...
                    var classDerivationChanged = new IdentifiedChange(WellKnownChangePriorities.TypeInternal,
						Severity.BreakingChange,
						new CodeDeltaDescriptor 
						{ 
							Reason = "Class no longer derives from " + firstClass.InheritsFrom.ToString(), 
							Was = first.ToCode(), 
							IsNow = second.ToCode() 
						});

					classDerivationChanged.ForType(first);

                    changes.Add(classDerivationChanged);
                }
			}
		}
	}

}

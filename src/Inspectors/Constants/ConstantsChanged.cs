using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
    public class ConstantsChanged : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CAC"; } }

		public string DisplayName { get { return "Changed Constants"; } }

		public string Description { get { return "Checks for changes to existing constants"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (firstClass.Constants.Any() || secondClass.Constants.Any())
			{
				var commonFields = firstClass.Constants.FuzzyInCommonWith(secondClass.Constants);

				foreach (var field in commonFields)
				{
					Constant oldConstant = field.Item1;
					Constant newConstant = field.Item2;

					if (oldConstant != null && newConstant != null)
					{
						// constant type has changed.
						if (oldConstant.ConstantType != newConstant.ConstantType)
                        {
                            var constantChanged = new IdentifiedChange(WellKnownChangePriorities.ConstantsChanged,
                                new CodeDeltaDescriptor 
                                { 
                                    Reason = string.Format("Changed type from {0} to {1}", oldConstant.ConstantType, newConstant.ConstantType), 
                                    Was = oldConstant.ToCode(), 
                                    IsNow = newConstant.ToCode() 
                                });

							constantChanged.ForType(first);

                            changes.Add(constantChanged);
                        }
					}
				}
			}
		}
	}
}

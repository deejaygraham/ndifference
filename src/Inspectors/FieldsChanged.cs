using NDifference.Analysis;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class FieldsChanged : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CAC"; } }

		public string DisplayName { get { return "Changed Fields"; } }

		public string Description { get { return "Checks for changes to existing fields"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Class || second.Taxonomy != TypeTaxonomy.Class)
				return;

			ClassDefinition firstClass = first as ClassDefinition;
			ClassDefinition secondClass = second as ClassDefinition;

			Debug.Assert(firstClass != null, "First type is not a class");
			Debug.Assert(secondClass != null, "Second type is not a class");

			if (firstClass.Fields.Any() || secondClass.Fields.Any())
			{
				var commonFields = firstClass.Fields.FuzzyInCommonWith(secondClass.Fields);

				foreach (var field in commonFields)
				{
					MemberField oldField = field.Item1;
					MemberField newField = field.Item2;

					if (oldField != null && newField != null)
					{
						// property type has changed.
						if (oldField.FieldType != newField.FieldType)
						{
							changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.FieldsChanged,
								new NamedDeltaDescriptor
								{
									Name = string.Format("Changed type from {0} to {1}", oldField.FieldType, newField.FieldType),
									Was = oldField.ToCode(),
									IsNow = newField.ToCode()
								}));
						}
					}
				}
			}
		}
	}

}

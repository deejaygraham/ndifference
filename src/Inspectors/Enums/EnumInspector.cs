// #define DEBUG_ENUMS

using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
	public class EnumInspector : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_CAIE"; } }

		public string DisplayName { get { return "Enum Changes"; } }

		public string Description { get { return "Checks for changes to enums"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy != TypeTaxonomy.Enum || second.Taxonomy != TypeTaxonomy.Enum)
				return;

			EnumDefinition firstEnum = first as EnumDefinition;
			EnumDefinition secondEnum = second as EnumDefinition;

			Debug.Assert(firstEnum != null, "First type is not an enum");
			Debug.Assert(secondEnum != null, "Second type is not an enum");

#if DEBUG_ENUMS

			string fileName = "C:\\" + firstEnum.Name.HtmlSafeTypeName() + ".txt";

			if (System.IO.File.Exists(fileName))
			    System.IO.File.Delete(fileName);
#endif

			// enums may be the same...
			if (this.AreSequencesEqual(firstEnum.AllowedValues, secondEnum.AllowedValues))
			{
				return;
			}

			var compareByTextAndValue = new CompareEnumByTextAndValue();

			var added = firstEnum.AllowedValues.AddedTo(secondEnum.AllowedValues, compareByTextAndValue);
			var removed = firstEnum.AllowedValues.RemovedFrom(secondEnum.AllowedValues, compareByTextAndValue);

			if (added.Any() || removed.Any())
			{
				// some removed and some added - have values changed because of an insert/deletion
				// in the middle of a sequence ?
				var compareByText = new CompareEnumByText();
				var insertInSequence = added.Except(removed, compareByText);

				if (insertInSequence.Any())
				{
					foreach (var insert in insertInSequence.OrderBy(x => x.Value))
                    {
                        // addition
                        var enumAdded = new IdentifiedChange(WellKnownChangePriorities.EnumValuesAdded,
                            new CodeDescriptor { Code = insert.ToCode() });

						enumAdded.ForType(first);

                        changes.Add(enumAdded);
                    }
				}

				var removeSequence = removed.Except(added, compareByText);

				if (removeSequence.Any())
				{
					foreach (var remove in removeSequence.OrderBy(x => x.Value))
                    {
                        // removed
                        var enumRemoved = new IdentifiedChange(WellKnownChangePriorities.EnumValuesRemoved,
                            new CodeDescriptor { Code = remove.ToCode() });

						enumRemoved.ForType(first);

                        changes.Add(enumRemoved);
                    }
				}

				if (!insertInSequence.Any() && !removeSequence.Any())
				{
					if (firstEnum.AllowedValues.Count == secondEnum.AllowedValues.Count)
					{
						// individual values may have been reassigned different explicit numeric values
						for (int i = 0; i < firstEnum.AllowedValues.Count; ++i)
						{
							var firstValue = firstEnum.AllowedValues[i];
							var secondValue = secondEnum.AllowedValues[i];

							if (firstValue.Value != secondValue.Value)
                            {
                                var enumValueChanged = new IdentifiedChange(WellKnownChangePriorities.EnumValuesChanged,
                                    new DeltaDescriptor { Was = firstValue.ToCode(), IsNow = secondValue.ToCode() });

								enumValueChanged.ForType(first);

                                changes.Add(enumValueChanged);
                            }
						}
					}
				}

#if DEBUG_ENUMS
				using (System.IO.Stream s = new System.IO.FileStream("C:\\" + firstEnum.Name.HtmlSafeTypeName() + ".txt", System.IO.FileMode.CreateNew))
			    {
			        using (System.IO.TextWriter writer = new System.IO.StreamWriter(s))
			        {
						writer.WriteLine("Removed " + firstEnum.FullName);

			            foreach (EnumValue v in removed)
			            {
			                writer.WriteLine(v.Name + " = " + v.Value);
			            }

			            writer.WriteLine("Added " + secondEnum.FullName);
			            foreach (EnumValue v in added)
			            {
			                writer.WriteLine(v.Name + " = " + v.Value);
			            }

			            writer.WriteLine("=============================");

			            foreach (EnumValue v in firstEnum.AllowedValues)
			            {
			                writer.WriteLine(v.Name + " = " + v.Value);
			            }

			            writer.WriteLine("=============================");

			            foreach (EnumValue v in secondEnum.AllowedValues)
			            {
			                writer.WriteLine(v.Name + " = " + v.Value);
			            }
			        }
			    }
#endif
			}
        }

        private bool AreSequencesEqual(IList<EnumValue> first, IList<EnumValue> second)
        {
            bool equal = first.Count == second.Count;

            if (equal)
            {
                // check individual items...
                for (int i = 0; i < first.Count; ++i)
                {
                    if (first[i] != second[i])
                    {
                        equal = false;
                        break;
                    }
                }
            }

            return equal;
        }
    }

    internal sealed class CompareEnumByTextAndValue : IEqualityComparer<EnumValue>
    {
        public bool Equals(EnumValue x, EnumValue y)
        {
            const int ExactMatch = 0;
            return string.Compare(
                                x.Name,
                                y.Name,
                                StringComparison.OrdinalIgnoreCase)
                                == ExactMatch && x.Value == y.Value;
        }

        public int GetHashCode(EnumValue obj)
        {
            return obj.ToString().GetHashCode() ^ obj.Value.GetHashCode();
        }
    }

    internal sealed class CompareEnumByText : IEqualityComparer<EnumValue>
    {
        public bool Equals(EnumValue x, EnumValue y)
        {
            const int ExactMatch = 0;
            return string.Compare(
                                x.Name,
                                y.Name,
                                StringComparison.OrdinalIgnoreCase)
                                == ExactMatch;
        }

        public int GetHashCode(EnumValue obj)
        {
            return obj.Name.GetHashCode();
        }
    }

    internal sealed class CompareEnumByValue : IEqualityComparer<EnumValue>
    {
        public bool Equals(EnumValue x, EnumValue y)
        {
            return x.Value == y.Value;
        }

        public int GetHashCode(EnumValue obj)
        {
            return obj.Value.GetHashCode();
        }
    }

}

using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Linq;

namespace NDifference.Inspectors
{
	public class PropertiesChanged : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_PAP"; } }

		public string DisplayName { get { return "Changed Properties"; } }

		public string Description { get { return "Checks for changes to existing properties"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				var commonProperties = firstRef.Properties(MemberVisibilityOption.Public).FuzzyInCommonWith(secondRef.Properties(MemberVisibilityOption.Public));

				if (commonProperties.Any())
				{
					foreach (var property in commonProperties)
					{
						MemberProperty oldProperty = property.Item1;
						MemberProperty newProperty = property.Item2;

						if (oldProperty.ExactlyMatches(newProperty))
							continue;

						if (oldProperty != null && newProperty != null)
						{
							if (oldProperty.GetterAccessibility == MemberAccessibility.Public && newProperty.GetterAccessibility != MemberAccessibility.Public)
                            {
                                var getterRemoved = new IdentifiedChange(WellKnownChangePriorities.PropertiesChanged,
                                    Severity.BreakingChange,
                                    new CodeDeltaDescriptor
                                    {
                                        Reason = "Get removed",
                                        Was = oldProperty.ToCode(),
                                        IsNow = newProperty.ToCode()
                                    });

                                getterRemoved.ForType(first);

								changes.Add(getterRemoved);
							}

							if (oldProperty.SetterAccessibility == MemberAccessibility.Public && newProperty.SetterAccessibility != MemberAccessibility.Public)
                            {
                                var setterRemoved = new IdentifiedChange(WellKnownChangePriorities.PropertiesChanged,
                                    Severity.BreakingChange,
                                    new CodeDeltaDescriptor
                                    {
                                        Reason = "Set removed",
                                        Was = oldProperty.ToCode(),
                                        IsNow = newProperty.ToCode()
                                    });

                                setterRemoved.ForType(first);

								changes.Add(setterRemoved);
                            }

							if (oldProperty.PropertyType != newProperty.PropertyType)
							{
								// type has moved namespaces

								if (oldProperty.PropertyType.ContainingNamespace.CompareTo(newProperty.PropertyType.ContainingNamespace) != 0
									&& oldProperty.PropertyType.Type == newProperty.PropertyType.Type)
                                {
                                    var movedNamespace = new IdentifiedChange(WellKnownChangePriorities.PropertiesChanged,
                                        Severity.BreakingChange,
                                        new CodeDeltaDescriptor
                                        {
                                            Reason = string.Format(
                                                "Property changed namespace from {0} to {1}",
                                                oldProperty.PropertyType.ContainingNamespace,
                                                newProperty.PropertyType.ContainingNamespace
                                            ),
                                            Was = oldProperty.ToCode(),
                                            IsNow = newProperty.ToCode()
                                        });

                                    movedNamespace.ForType(first);

									changes.Add(movedNamespace);
                                }
								else
                                {
                                    var propertyTypeChanged = new IdentifiedChange(WellKnownChangePriorities.PropertiesChanged,
                                        Severity.BreakingChange,
                                        new CodeDeltaDescriptor
                                        {
                                            Reason = string.Format(
                                                "Property changed from {0} to {1}",
                                                oldProperty.PropertyType.Type,
                                                newProperty.PropertyType.Type
                                            ),
                                            Was = oldProperty.ToCode(),
                                            IsNow = newProperty.ToCode()
                                        });

                                    propertyTypeChanged.ForType(first);

                                    changes.Add(propertyTypeChanged);
                                }
							}
						}
                    }
				}
			}
		}
	}

}

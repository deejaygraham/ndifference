using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;

namespace NDifference.Inspectors
{
    public class MethodsChanged : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_MAM"; } }

		public string DisplayName { get { return "Changed Methods"; } }

		public string Description { get { return "Checks for changes to existing methods"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				// look for non-overloaded methods that have a counterpart in the new version...
				foreach (var method in firstRef.AllMethods)
				{
					var counterpart = secondRef.AllMethods.FindExactMatchFor(method);

					if (counterpart != null)
					{
						if (method.IsVirtual != counterpart.IsVirtual)
                        {
                            var virtualnessChanged = new IdentifiedChange(WellKnownChangePriorities.MethodsChanged,
                                Severity.BreakingChange,
                                new ChangedCodeSignature
                                {
                                    Reason = method.IsVirtual ? "Method is no longer virtual" : "Method is now virtual",
                                    Was = method.ToCode(),
                                    IsNow = counterpart.ToCode()
                                });

                            virtualnessChanged.ForType(first);

                            changes.Add(virtualnessChanged);
                        }
						else if (!method.IsAbstract && counterpart.IsAbstract)
                        {
                            var methodMadeAbstract = new IdentifiedChange(WellKnownChangePriorities.MethodsChanged,
                                Severity.BreakingChange,
                                new ChangedCodeSignature
                                {
                                    Reason = "Method is now abstract",
                                    Was = method.ToCode(),
                                    IsNow = counterpart.ToCode()
                                });

                            methodMadeAbstract.ForType(first);

                            changes.Add(methodMadeAbstract);
                        }

						if (method.IsStatic != counterpart.IsStatic)
                        {
                            var staticnessChanged = new IdentifiedChange(WellKnownChangePriorities.MethodsChanged,
                                Severity.BreakingChange,
                                new ChangedCodeSignature
                                {
                                    Reason = method.IsStatic ? "Method is no longer static" : "Method is now static",
                                    Was = method.ToCode(),
                                    IsNow = counterpart.ToCode()
                                });
                            
                            staticnessChanged.ForType(first);

                            changes.Add(staticnessChanged);
                        }

						if (method.Accessibility != counterpart.Accessibility)
                        {
                            var accessibilityChanged = new IdentifiedChange(WellKnownChangePriorities.MethodsChanged,
                                Severity.BreakingChange,
                                new ChangedCodeSignature
                                {
                                    Reason = string.Format("Accessibility has changed from {0} to {1}", 
                                        method.Accessibility.ToDescription(),
                                        counterpart.Accessibility.ToDescription()),
                                    Was = method.ToCode(),
                                    IsNow = counterpart.ToCode()
                                });

                            accessibilityChanged.ForType(first);

                            changes.Add(accessibilityChanged);
                        }

						MemberMethod mm = method as MemberMethod;
						MemberMethod cm = counterpart as MemberMethod;

						if (mm != null && cm != null)
						{
							if (mm.ReturnType != cm.ReturnType)
                            {
                                var returnTypeChanged = new IdentifiedChange(WellKnownChangePriorities.MethodsChanged,
                                    Severity.BreakingChange,
                                    new ChangedCodeSignature
                                    {
                                        Reason = "Return type has changed",
                                        Was = mm.ToCode(),
                                        IsNow = cm.ToCode()
                                    });

                                returnTypeChanged.ForType(first);

                                changes.Add(returnTypeChanged);
                            }
						}

						// other things have changed ???
					}
				}
			}
		}
	}

}

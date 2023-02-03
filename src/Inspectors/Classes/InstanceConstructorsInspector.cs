using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace NDifference.Inspectors
{
    public class InstanceConstructorsInspector : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_ICC006"; } }

		public string DisplayName { get { return "Instance Constructors"; } }

		public string Description { get { return "Looks for constructor changes."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Class)
			{
				ClassDefinition cd1 = first as ClassDefinition;
				ClassDefinition cd2 = second as ClassDefinition;

				var wasConstructors = cd1.Constructors;
				var nowConstructors = cd2.Constructors;

				var comparer = new ConstructorComparer();

				var onlyInOld = wasConstructors.RemovedFrom(nowConstructors, comparer);
				var onlyInNew = nowConstructors.RemovedFrom(wasConstructors, comparer);

				if (onlyInOld.Count() == 1 && onlyInNew.Count() == 1)
				{
					// guessing constructor has changed
					InstanceConstructor oldCtor = onlyInOld.First();
					InstanceConstructor newCtor = onlyInNew.First();

					if (!newCtor.Signature.ExactlyMatches(oldCtor.Signature))
                    {
                        var constructorChanged = new IdentifiedChange(WellKnownChangePriorities.ConstructorsChanged,
							Severity.BreakingChange,
							new ChangedCodeSignature
							{ 
								Was = oldCtor.ToCode(),
								IsNow = newCtor.ToCode(),
								Reason = "Constructor signature changed"
							});

						constructorChanged.ForType(first);

                        changes.Add(constructorChanged);
                    }
				}
				else
				{
					// otherwise we just report which ones have been 
					// removed and which ones have been added.
					foreach (var remove in onlyInOld)
                    {
                        var constructorRemoved = new IdentifiedChange(WellKnownChangePriorities.ConstructorsRemoved,
							Severity.BreakingChange,
							new RemovedSignature 
							{ 
								Signature = remove.ToCode(),
								Reason = "Constructor removed"
							});

						constructorRemoved.ForType(first);

                        changes.Add(constructorRemoved);
                    }
					
					foreach (var add in onlyInNew)
                    {
                        var constructorAdded = new IdentifiedChange(WellKnownChangePriorities.ConstructorsAdded,
							Severity.NonBreaking,
							new AddedSignature()
							{
								Signature = add.ToCode()
							});

						constructorAdded.ForType(first);

                        changes.Add(constructorAdded);
                    }
				}
			}
		}
	}

	public class ConstructorComparer : IEqualityComparer<InstanceConstructor>
	{
		public bool Equals(InstanceConstructor x, InstanceConstructor y)
		{
			return x.Signature.ExactlyMatches(y.Signature);
		}

		public int GetHashCode(InstanceConstructor obj)
		{
			return obj.Signature.GetHashCode();
		}
	}
}

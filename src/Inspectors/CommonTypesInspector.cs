using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Checks types in an assembly to find differences used for further analysis.
	/// </summary>
	public class CommonTypesInspector : ITypeCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TCI002"; } }

		public string DisplayName { get { return "Common Types"; } }

		public string Description { get { return "Checks for common types between two versions of the same assembly"; } }

		public void Inspect(ICombinedTypes types, IdentifiedChangeCollection changes)
		{
			Debug.Assert(types != null, "List of types cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

            var changedInCommon = types.ChangedInCommon;

            if (changedInCommon.Any())
            {
               // changes.Add(WellKnownAssemblyCategories.PotentiallyChangedTypes);

                foreach (var common in changedInCommon)
                {
                    //changes.Add(new IdentifiedChange(this, WellKnownAssemblyCategories.PotentiallyChangedTypes, common.Second.FullName, new DocumentLink
                    //{
                    //    LinkText = common.First.FullName,
                    //    LinkUrl = common.First.FullName,
                    //    Identifier = common.First.Identifier
                    //}));
                }
            }
        }
	}
}

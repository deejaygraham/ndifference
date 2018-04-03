﻿using NDifference.Analysis;
using NDifference.Inspection;
using System.Diagnostics;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Looking for assemblies in first that are not in second.
	/// </summary>
	public class RemovedAssembliesInspector : IAssemblyCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "ACI003"; } }

		public string DisplayName { get { return "Removed Assemblies"; } }

		public string Description { get { return "Checks for removed assemblies"; } }

		public void Inspect(ICombinedAssemblies assemblies, IdentifiedChangeCollection changes)
		{
			Debug.Assert(assemblies != null, "List of assemblies cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

			changes.Add(WellKnownSummaryCategories.RemovedAssemblies);

			foreach (var removed in assemblies.InEarlierOnly)
			{
				changes.Add(new IdentifiedChange(this, WellKnownSummaryCategories.RemovedAssemblies, removed.First.Name)); 
			}
		}
	}
}
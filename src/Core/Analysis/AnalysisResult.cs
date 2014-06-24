using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Analysis
{
	public class AnalysisResult
	{
		private IdentifiedChangeCollection _topLevelChanges = new IdentifiedChangeCollection();

		private List<IdentifiedChangeCollection> _assemblyLevelChanges = new List<IdentifiedChangeCollection>();

		private List<IdentifiedChangeCollection> _typeLevelChanges = new List<IdentifiedChangeCollection>();

		public bool Cancelled { get; set; }

		public IdentifiedChangeCollection Summary
		{
			get
			{
				return this._topLevelChanges;
			}
		}

		public ReadOnlyCollection<IdentifiedChangeCollection> AssemblyLevelChanges
		{
			get
			{
				return new ReadOnlyCollection<IdentifiedChangeCollection>(this._assemblyLevelChanges);
			}
		}

		public ReadOnlyCollection<IdentifiedChangeCollection> TypeLevelChanges
		{
			get
			{
				return new ReadOnlyCollection<IdentifiedChangeCollection>(this._typeLevelChanges);
			}
		}

		public void ReplaceSummary(IdentifiedChangeCollection summary)
		{
			this._topLevelChanges = summary;
		}

		public void Assembly(IdentifiedChangeCollection assembly)
		{
			this._assemblyLevelChanges.Add(assembly);
		}

		public void Type(IdentifiedChangeCollection type)
		{
			this._typeLevelChanges.Add(type);
		}
	}
}

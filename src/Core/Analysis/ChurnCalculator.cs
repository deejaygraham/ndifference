using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Analysis
{
	/// <summary>
	/// Calculates a measure of how much has changed in a class, assembly or in total.
	/// </summary>
	public class ChurnCalculator : IChurnable
	{
		public ChurnCalculator()
		{
		}

		/// <summary>
		/// No changes to assemblies, just increment total.
		/// </summary>
		/// <param name="oldVersion"></param>
		/// <param name="newVersion"></param>
		public ChurnCalculator(IChurnable churn)
		{
			this.Total = churn.Total;
			this.Removed = churn.Removed;
			this.Added = churn.Added;
			this.Changed = churn.Changed;
		}

		public int Total { get; private set; }

		public int Removed { get; private set; }

		public int Added { get; private set; }

		public int Changed { get; private set; }

		public void Increment(IChurnable churn)
		{
			this.IncrementAdded(churn.Added);
			this.IncrementChanged(churn.Changed);
			this.IncrementRemoved(churn.Removed);
			this.IncrementTotal(churn.Total);
		}

		public void IncrementTotal(int by)
		{
			this.Total += by;
		}

		public void IncrementRemoved(int by)
		{
			this.Removed += by;
		}

		public void IncrementAdded(int by)
		{
			this.Added += by;
		}

		public void IncrementChanged(int by)
		{
			this.Changed += by;
		}

		/// <summary>
		/// Calculates the percentage churn.
		/// </summary>
		/// <returns></returns>
		public int Calculate()
		{
			this.Total = Math.Max(this.Added + this.Removed, this.Total);

			if (this.Total == 0)
				return 0;

			return (100 * (this.Added + this.Removed + (2 * this.Changed))) / this.Total;
		}
	}
}

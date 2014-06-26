using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Analysis
{
	/// <summary>
	/// Represents a metric for "churn" in an assembly, type or generally.
	/// </summary>
	public interface IChurnable
	{
		int Total { get; }

		int Removed { get; }

		int Added { get; }

		int Changed { get; }
	}
}

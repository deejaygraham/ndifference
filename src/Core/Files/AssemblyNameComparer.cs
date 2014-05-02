using System;
using System.Collections.Generic;

namespace NDifference
{
	/// <summary>
	/// Compare based on name only.
	/// </summary>
	internal sealed class AssemblyNameComparer : IEqualityComparer<IAssemblyInfo>, IEqualityComparer<IAssemblyDiskInfo>
	{
		public bool Equals(IAssemblyInfo x, IAssemblyInfo y)
		{
			return IsExactMatch(x.Name, y.Name);
		}

		public bool Equals(IAssemblyInfo x, string y)
		{
			return IsExactMatch(x.Name, y);
		}

		public int GetHashCode(IAssemblyInfo obj)
		{
			return obj.Name.GetHashCode();
		}

		public bool Equals(IAssemblyDiskInfo x, IAssemblyDiskInfo y)
		{
			return IsExactMatch(x.Name, y.Name);
		}

		public bool Equals(IAssemblyDiskInfo x, string y)
		{
			return IsExactMatch(x.Name, y);
		}

		public int GetHashCode(IAssemblyDiskInfo obj)
		{
			return obj.Name.GetHashCode();
		}

		private bool IsExactMatch(string name1, string name2)
		{
			const int ExactMatch = 0;
			bool same = string.Compare(
								name1,
								name2,
								StringComparison.OrdinalIgnoreCase)
								== ExactMatch;

			return same;
		}
	}
}

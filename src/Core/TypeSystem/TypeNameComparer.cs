using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// Comparison based on full type name.
	/// </summary>
	public class TypeNameComparer : IEqualityComparer<ITypeInfo>
	{
		public bool Equals(ITypeInfo x, ITypeInfo y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}

			if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
			{
				return false;
			}

			return x.FullName.CompareTo(y.FullName) == 0;
		}

		public int GetHashCode(ITypeInfo obj)
		{
			return obj.FullName.GetHashCode();
		}
	}
}

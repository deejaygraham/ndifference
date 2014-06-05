using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	/// <summary>
	/// Supports a hash-type method of checking binary differences.
	/// </summary>
	public interface IHashable
	{
		/// <summary>
		/// Create a hash representation of this object.
		/// </summary>
		/// <returns></returns>
		string CalculateHash();
	}
}

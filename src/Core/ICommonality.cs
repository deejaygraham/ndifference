using System.Collections.Generic;

namespace NDifference
{
	/// <summary>
	/// 
	/// </summary>
	public interface ICommonality<T> where T : class
	{
		IEnumerable<T> InCommonWith(ICommonality<T> other, IEqualityComparer<T> comparison);
	}

	public interface IAdditions<T> where T : class
	{
		IEnumerable<T> AddedTo(ICommonality<T> other, IEqualityComparer<T> comparison);
	}
}

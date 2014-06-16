using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	public interface IExactlyMatch<T>
	{
		bool Matches(T other);
	}

	public static class IExactlyMatchExtensions
	{
		public static bool ContainsExactMatchFor<T>(this IEnumerable<T> collection, T matchCandidate)
							where T : IExactlyMatch<T>
		{
			bool isExactMatch = false;

			foreach (T item in collection)
			{
				if (item.Matches(matchCandidate))
				{
					isExactMatch = true;
					break;
				}
			}

			return isExactMatch;
		}

		public static T FindExactMatchFor<T>(this IEnumerable<T> collection, T matchCandidate)
					where T : IExactlyMatch<T>
		{
			T matched = default(T);

			foreach (T item in collection)
			{
				if (item.Matches(matchCandidate))
				{
					matched = item;
					break;
				}
			}

			return matched;
		}
	}

}

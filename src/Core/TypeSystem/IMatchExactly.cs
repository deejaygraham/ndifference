using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	public interface IMatchExactly<T>
	{
		bool ExactlyMatches(T other);
	}

	public static class IMatchExactlyExtensions
	{
		public static bool ContainsExactMatchFor<T>(this IEnumerable<T> collection, T matchCandidate)
							where T : IMatchExactly<T>
		{
			bool isExactMatch = false;

			foreach (T item in collection)
			{
				if (item.ExactlyMatches(matchCandidate))
				{
					isExactMatch = true;
					break;
				}
			}

			return isExactMatch;
		}

		public static T FindExactMatchFor<T>(this IEnumerable<T> collection, T matchCandidate)
					where T : IMatchExactly<T>
		{
			T matched = default(T);

			foreach (T item in collection)
			{
				if (item.ExactlyMatches(matchCandidate))
				{
					matched = item;
					break;
				}
			}

			return matched;
		}
	}
}

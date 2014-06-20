using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	public interface IMatchFuzzily<T>
	{
		bool FuzzyMatches(T other);
	}

	public static class IMatchFuzzilyExtensions
	{
		public static bool ContainsFuzzyMatchFor<T>(this IEnumerable<T> collection, T matchCandidate)
			where T : IMatchFuzzily<T>
		{
			return collection.Any(x => x.FuzzyMatches(matchCandidate));
		}

		public static T FindFuzzyMatchFor<T>(this IEnumerable<T> collection, T matchCandidate)
			where T : IMatchFuzzily<T>
		{
			return collection.FirstOrDefault(x => x.FuzzyMatches(matchCandidate));
		}

		public static IEnumerable<T> FindAllFuzzyMatchesFor<T>(this IEnumerable<T> collection, T matchCandidate)
					where T : IMatchFuzzily<T>
		{
			return collection.Where(x => x.FuzzyMatches(matchCandidate));
		}

		public static Collection<Tuple<T, T>> FuzzyInCommonWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
			where T : IMatchFuzzily<T>
		{
			var matches = new Collection<Tuple<T, T>>();

			foreach (var item in first)
			{
				T match = second.FindFuzzyMatchFor(item);

				if (match != null)
				{
					matches.Add(new Tuple<T, T>(item, match));
				}
			}

			return matches;
		}

	}
}

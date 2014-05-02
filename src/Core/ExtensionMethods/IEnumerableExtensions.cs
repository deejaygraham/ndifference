using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	/// <summary>
	/// IEnumerable extension methods used for finding common, added and removed objects.
	/// </summary>
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Returns a collection of objects found in each list.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static IEnumerable<T> InCommonWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			Debug.Assert(first != null, "first cannot be null");
			Debug.Assert(second != null, "second cannot be null");

			return first.Intersect(second);
		}

		/// <summary>
		/// Returns a collection of objects found in each list using custom comparison
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static IEnumerable<T> InCommonWith<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
		{
			Debug.Assert(first != null, "first cannot be null");
			Debug.Assert(second != null, "second cannot be null");
			Debug.Assert(comparer != null, "comparer cannot be null");

			return first.Intersect(second, comparer);
		}

		/// <summary>
		/// Returns a collection of objects found in each list using custom comparison and custom projection.
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="comparer"></param>
		/// <param name="projector"></param>
		/// <returns></returns>
		public static ICollection<TResult> InCommonWith<TSource, TResult>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer, Func<TSource, TResult> projector)
		{
			Debug.Assert(first != null, "first cannot be null");
			Debug.Assert(second != null, "second cannot be null");
			Debug.Assert(comparer != null, "comparer cannot be null");
			Debug.Assert(projector != null, "projector cannot be null");

			var intersection = first.Intersect(second, comparer);

			return new List<TResult>(intersection.Select(projector));
		}

		/// <summary>
		/// Returns a collection of objects found in first list and not in second.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static IEnumerable<T> RemovedFrom<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			return first.Except(second);
		}

		/// <summary>
		/// Returns a collection of objects found in first list and not in second using custom comparison.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static IEnumerable<T> RemovedFrom<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
		{
			return first.Except(second, comparer);
		}

		/// <summary>
		/// Returns a collection of objects found in first list and not in second using custom comparison and projection.
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="comparer"></param>
		/// <param name="projector"></param>
		/// <returns></returns>
		public static ICollection<TResult> RemovedFrom<TSource, TResult>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer, Func<TSource, TResult> projector)
		{
			Debug.Assert(first != null, "first cannot be null");
			Debug.Assert(second != null, "second cannot be null");
			Debug.Assert(comparer != null, "comparer cannot be null");
			Debug.Assert(projector != null, "projector cannot be null");

			var intersection = first.Except(second, comparer);

			return new List<TResult>(intersection.Select(projector));
		}

		/// <summary>
		/// Returns a collection of objects found in second list and not in first.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static IEnumerable<T> AddedTo<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			return second.Except(first);
		}

		/// <summary>
		/// Returns a collection of objects found in second list and not in first using custom comparison.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static IEnumerable<T> AddedTo<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
		{
			return second.Except(first, comparer);
		}
		
		/// <summary>
		/// Find a match for a specific item in a list.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="instance"></param>
		/// <param name="comparison"></param>
		/// <returns></returns>
		public static T FindMatchFor<T>(this IEnumerable<T> list, T instance, IEqualityComparer<T> comparison)
		{
			return list.FirstOrDefault(x => comparison.Equals(x, instance));
		}
	}
}

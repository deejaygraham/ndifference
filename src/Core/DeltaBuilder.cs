using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference
{
	/// <summary>
	/// Simple delta algorithm. 
	/// Look for all things that have been added to second collection compared to first.
	/// Look for all things that have been removed from first collection compared to second.
	/// Look for commonality and inspect if common objects have changed compared one to another.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class DeltaBuilder<T> where T : IEquatable<T>
	{
		public static Delta<T, T> Build(IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
		{
			Debug.Assert(first != null, "First list of objects cannot be null");
			Debug.Assert(second != null, "Second list of objects cannot be null");
			Debug.Assert(comparer != null, "comparer object cannot be null");

			var delta = new Delta<T, T>();

            foreach (var added in first.AddedTo(second, comparer))
            {
				delta.Added(added);
            }

			foreach (var removed in first.RemovedFrom(second, comparer))
            {
				delta.Removed(removed);
            }

			foreach (var common in first.InCommonWith(second, comparer))
            {
                var oldVersion = first.FindMatchFor(common, comparer);
                var newVersion = second.FindMatchFor(common, comparer);

                if (oldVersion == null || newVersion == null)
                {
                    Debug.Assert(oldVersion != null && newVersion != null, "Mismatch finding common objects");
                    continue;
                }

                if (oldVersion.Equals(newVersion))
                {
                    // if there's an exact match in all respects -
                    // this may be the case if we're using 
                    // the same version in two separate instances 
					delta.Unchanged(newVersion);
                }
                else
                {
                    // something non-specific has changed.
                    // Needs further investigation...
					delta.Changed(new Pair<T> { First = oldVersion, Second = newVersion });
                }
            }

			return delta;
		}
	}
}

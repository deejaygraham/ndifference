using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	public interface IMemberInfo : ISourceCodeProvider, IMaybeObsolete
	{
		MemberAccessibility Accessibility { get; }
	}

	public static class IMemberInfoExtensions
	{
		public static Collection<T> FindRemovedMembers<T>(this IEnumerable<T> later, IEnumerable<T> earlier)
			where T : IMatchFuzzily<T>
		{
			return new Collection<T>(earlier.Where(x => !later.ContainsFuzzyMatchFor(x)).ToList());
		}

		public static Collection<T> FindAddedMembers<T>(this IEnumerable<T> later, IEnumerable<T> earlier)
			where T : IMatchFuzzily<T>
		{
			return new Collection<T>(later.Where(x => !earlier.ContainsFuzzyMatchFor(x)).ToList());
		}

		public static Collection<T> FindObsoleteMembers<T>(this IEnumerable<T> later)
			where T : IMaybeObsolete
		{
			return new Collection<T>(later.Where(x => x.ObsoleteMarker != null).ToList());
		}
	}
}

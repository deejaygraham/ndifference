using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	public interface IMemberMethod : IMemberInfo, ISourceCodeProvider, IMatchExactly<IMemberMethod>, IMatchFuzzily<IMemberMethod>
	{
		Signature Signature { get; }

		bool IsAbstract { get; }

		bool IsStatic { get; }

		bool IsVirtual { get; }
	}

	public static class IMemberMethodExtensions
	{
		/// <summary>
		/// List of all "distinct" methods in the collection.
		/// Overloaded methods are represented by a single entry.
		/// </summary>
		public static IList<string> DistinctMethods(this IEnumerable<IMemberMethod> methods)
		{
			HashSet<string> distinct = new HashSet<string>();

			foreach (var method in methods)
			{
				distinct.Add(method.Signature.Name);
			}

			return distinct.ToList();
		}

		/// <summary>
		/// List names of methods which have overloads in the collection.
		/// </summary>
		public static IList<string> OverloadedMethods(this IEnumerable<IMemberMethod> methods)
		{
			List<string> overloads = new List<string>();

			overloads.AddRange(methods.DistinctMethods().Where(x => methods.OverloadsFor(x).Count() > 1));

			return overloads;
		}

		/// <summary>
		/// List methods which do not have overloads in the collection.
		/// </summary>
		public static IEnumerable<IMemberMethod> NonOverloadedMethods(this IEnumerable<IMemberMethod> methods)
		{
			var notOverloaded = new List<IMemberMethod>();

			foreach (var method in methods)
			{
				var existing = methods.OverloadsFor(method);

				if (existing != null && existing.Count() == 1)
				{
					notOverloaded.Add(method);
				}
			}

			return notOverloaded;
		}

		public static IEnumerable<Signature> OverloadsFor(this IEnumerable<IMemberMethod> methods, string methodName)
		{
			var overloads = new List<Signature>();

			foreach (var method in methods)
			{
				if (String.Compare(method.Signature.Name, methodName, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					overloads.Add(method.Signature);
				}
			}

			return overloads;
		}

		public static IEnumerable<Signature> OverloadsFor(this IEnumerable<IMemberMethod> methods, IMemberMethod m)
		{
			return methods.OverloadsFor(m.Signature.Name);
		}

		public static void Sort(this IEnumerable<IMemberMethod> methods)
		{
			methods.OrderBy(x => x.Signature.Name);
		}
	}
}

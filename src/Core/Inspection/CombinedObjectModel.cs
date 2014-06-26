using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NDifference.Inspection
{
	public class CombinedObjectModel : ICombinedTypes
	{
		private List<Pair<ITypeInfo>> typeList = new List<Pair<ITypeInfo>>();

		public IEnumerable<Pair<ITypeInfo>> Types
		{
			get
			{
				return new ReadOnlyCollection<Pair<ITypeInfo>>(typeList);
			}
		}

		public IEnumerable<Pair<ITypeInfo>> InCommon
		{
			get
			{
				return new ReadOnlyCollection<Pair<ITypeInfo>>(typeList.Where(x =>
					x.First != null
					&& x.Second != null
					).ToList());
			}
		}

		public IEnumerable<Pair<ITypeInfo>> ChangedInCommon
		{
			get
			{
				return new ReadOnlyCollection<Pair<ITypeInfo>>(typeList.Where(x =>
					x.First != null
					&& x.Second != null
					&& !HashesMatch(x.First, x.Second)
					).ToList());
			}
		}

		private bool HashesMatch(ITypeInfo first, ITypeInfo second)
		{
			if (first == null || second == null)
			{
				return false;
			}

			return first.CalculateHash().Equals(second.CalculateHash());
		}

		public IEnumerable<Pair<ITypeInfo>> InEarlierOnly
		{
			get
			{
				return new ReadOnlyCollection<Pair<ITypeInfo>>(typeList.Where(x =>
					x.First != null
					&& x.Second == null
					).ToList());
			}
		}

		public IEnumerable<Pair<ITypeInfo>> InLaterOnly
		{
			get
			{
				return new ReadOnlyCollection<Pair<ITypeInfo>>(typeList.Where(x =>
					x.First == null
					&& x.Second != null
					).ToList());
			}
		}
		
		public int Total
		{
			get
			{
				return (this.Changed * 2) + this.Removed + this.Added;
			}
		}

		public int Removed
		{
			get
			{
				return this.typeList.Count(x => x.Second == null);
			}
		}

		public int Added
		{
			get
			{
				return this.typeList.Count(x => x.First == null);
			}
		}

		public int Changed
		{
			get
			{
				return this.typeList.Count(x => 
					x.First != null 
					&& x.Second != null 
					&& !HashesMatch(x.First, x.Second)
					);
			}
		}

		public void Add(IEnumerable<ITypeInfo> first, IEnumerable<ITypeInfo> second)
		{
			var comparer = new TypeNameComparer();

			foreach (ITypeInfo added in first.AddedTo(second, comparer))
			{
				typeList.Add(new Pair<ITypeInfo>(null, added));
			}

			foreach (ITypeInfo removed in first.RemovedFrom(second, comparer))
			{
				typeList.Add(new Pair<ITypeInfo>(removed, null));
			}
				
			foreach (ITypeInfo common in first.InCommonWith(second, comparer))
			{
				var oldVersion = first.FindMatchFor(common, comparer);
				var newVersion = second.FindMatchFor(common, comparer);

				typeList.Add(new Pair<ITypeInfo>(oldVersion, newVersion));
			}
		}

		public static CombinedObjectModel BuildFrom(IEnumerable<ITypeInfo> first, IEnumerable<ITypeInfo> second)
		{
			var model = new CombinedObjectModel();

			model.Add(first, second);

			return model;
		}
	}
}

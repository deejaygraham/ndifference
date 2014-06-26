using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NDifference.Inspection
{
	public class CombinedAssemblyModel : ICombinedAssemblies
	{
		private List<Pair<IAssemblyDiskInfo>> adiList = new List<Pair<IAssemblyDiskInfo>>();

		public IEnumerable<Pair<IAssemblyDiskInfo>> Assemblies
		{
			get
			{
				return new ReadOnlyCollection<Pair<IAssemblyDiskInfo>>(adiList);
			}
		}

		public IEnumerable<Pair<IAssemblyDiskInfo>> InCommon
		{
			get
			{
				return new ReadOnlyCollection<Pair<IAssemblyDiskInfo>>(adiList.Where(x => 
					x.First != null 
					&& x.Second != null
					).ToList());
			}
		}

		public IEnumerable<Pair<IAssemblyDiskInfo>> ChangedInCommon
		{
			get
			{
				return new ReadOnlyCollection<Pair<IAssemblyDiskInfo>>(adiList.Where(x =>
					x.First != null
					&& x.Second != null
					&& !ChecksumsMatch(x.First, x.Second)
					).ToList());
			}
		}


		private bool ChecksumsMatch(IAssemblyDiskInfo first, IAssemblyDiskInfo second)
		{
			if (first == null || second == null)
				return false;

			return first.Equals(second);
		}

		public IEnumerable<Pair<IAssemblyDiskInfo>> InEarlierOnly
		{
			get
			{
				return new ReadOnlyCollection<Pair<IAssemblyDiskInfo>>(adiList.Where(x =>
					x.First != null
					&& x.Second == null
					).ToList());
			}
		}

		public IEnumerable<Pair<IAssemblyDiskInfo>> InLaterOnly
		{
			get
			{
				return new ReadOnlyCollection<Pair<IAssemblyDiskInfo>>(adiList.Where(x =>
					x.First == null
					&& x.Second != null
					).ToList());
			}
		}

		public int Total
		{
			get 
			{
				return (this.Changed * 2) + this.Added + this.Removed;
			}
		}

		public int Removed
		{
			get
			{
				return this.adiList.Count(x => x.Second == null);
			}
		}

		public int Added
		{
			get
			{
				return this.adiList.Count(x => x.First == null);
			}
		}

		public int Changed
		{
			get
			{
				return this.adiList.Count(x => 
					x.First != null 
					&& x.Second != null
					&& !ChecksumsMatch(x.First, x.Second));
			}
		}

		public void Add(IEnumerable<IAssemblyDiskInfo> first, IEnumerable<IAssemblyDiskInfo> second)
		{
			var comparer = new AssemblyNameComparer();

			foreach (IAssemblyDiskInfo added in first.AddedTo(second, comparer))
			{
				adiList.Add(new Pair<IAssemblyDiskInfo>(null, added));
			}

			foreach (IAssemblyDiskInfo removed in first.RemovedFrom(second, comparer))
			{
				adiList.Add(new Pair<IAssemblyDiskInfo>(removed, null));
			}

			foreach (IAssemblyDiskInfo common in first.InCommonWith(second, comparer))
			{
				var oldVersion = first.FindMatchFor(common);
				var newVersion = second.FindMatchFor(common);

				adiList.Add(new Pair<IAssemblyDiskInfo>(oldVersion, newVersion));
			}
		}

		public static CombinedAssemblyModel BuildFrom(IEnumerable<IAssemblyDiskInfo> first, IEnumerable<IAssemblyDiskInfo> second)
		{
			var model = new CombinedAssemblyModel();

			model.Add(first, second);

			return model;
		}
	}
}

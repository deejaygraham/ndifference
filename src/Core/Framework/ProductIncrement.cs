using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Framework
{
	/// <summary>
	/// Represents one version/release of a product
	/// </summary>
	[DebuggerDisplay("{Name}")]
	public class ProductIncrement
	{
		private HashSet<IAssemblyDiskInfo> assemblies = new HashSet<IAssemblyDiskInfo>();

		public ProductIncrement()
		{
		}

		public ProductIncrement(string friendlyName)
		{
			Debug.Assert(!String.IsNullOrEmpty(friendlyName), "Name cannot be blank");

			this.Name = friendlyName;
		}

		/// <summary>
		/// Name of this release
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The assemblies that go together to make this release.
		/// </summary>
		public ReadOnlyCollection<IAssemblyDiskInfo> Assemblies
		{
			get
			{
				Debug.Assert(this.assemblies != null, "Assembly collection cannot be null");

				return new ReadOnlyCollection<IAssemblyDiskInfo>(this.assemblies.ToList());
			}
		}

		public void AddRange(IEnumerable<IAssemblyDiskInfo> packages)
		{
			foreach (var package in packages)
			{
				this.Add(package);
			}
		}

		public void Add(IAssemblyDiskInfo assembly)
		{
			Debug.Assert(assembly != null, "Assembly cannot be null");
			Debug.Assert(!string.IsNullOrEmpty(assembly.Path), "Assembly path cannot be blank");

			this.assemblies.Add(assembly);
		}
	}

}

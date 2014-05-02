using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	/// <summary>
	/// A simple assembly before reflection/introspection for types.
	/// </summary>
	[DebuggerDisplay("{Name}")]
	[Obsolete("Use AssemblyInfo or AssemblyDiskInfo")]
	public sealed class OpaqueAssembly : IEquatable<OpaqueAssembly>, IUniquelyIdentifiable
	{
		private Identifier ident = new Identifier();

		public OpaqueAssembly()
		{
		}

		public OpaqueAssembly(string path)
			: this(path, DateTime.MinValue, 0, string.Empty)
		{
		}

		public OpaqueAssembly(string folder, string file)
			: this(System.IO.Path.Combine(folder, file))
		{
		}

		public OpaqueAssembly(string path, DateTime date, long size, string checksum)
		{
			Debug.Assert(!string.IsNullOrEmpty(path), "Path cannot be blank");

			this.Path = path;
			this.Name = System.IO.Path.GetFileName(path);
			this.Date = date;
			this.Size = size;
			this.Checksum = checksum;
		}

		public string Identifier
		{
			get
			{
				return this.ident;
			}

			set
			{
				this.ident = value;
			}
		}

		public string Name { get; set; }

		public string Path { get; set; }

		public DateTime Date { get; private set; }

		public long Size { get; private set; }

		public string Checksum { get; private set; }

		/// <summary>
		/// Equal based on everything but path.
		/// </summary>
		/// <param name="leftHandSide">left hand side of comparison</param>
		/// <param name="rightHandSide">right hand side of comparison</param>
		/// <returns>true if equal</returns>
		public static bool operator ==(OpaqueAssembly leftHandSide, OpaqueAssembly rightHandSide)
		{
			if (object.ReferenceEquals(leftHandSide, rightHandSide))
			{
				return true;
			}

			if (((object)leftHandSide == null) || ((object)rightHandSide == null))
			{
				return false;
			}

			return leftHandSide.DetailsMatch(rightHandSide);
		}

		public static bool operator !=(OpaqueAssembly leftHandSide, OpaqueAssembly rightHandSide)
		{
			return !(leftHandSide == rightHandSide);
		}

		public override string ToString()
		{
			return this.Name;
		}

		public override int GetHashCode()
		{
			return this.Checksum.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			OpaqueAssembly otherAssembly = obj as OpaqueAssembly;

			if ((object)otherAssembly == null)
			{
				return false;
			}

			return this.DetailsMatch(otherAssembly);
		}

		public bool Equals(OpaqueAssembly other)
		{
			if ((object)other == null)
			{
				return false;
			}

			return this.DetailsMatch(other);
		}

		private bool DetailsMatch(OpaqueAssembly other)
		{
			return this.Name == other.Name && this.Size == other.Size
				&& this.Date == other.Date && this.Checksum == other.Checksum;
		}
	}
	
	/// <summary>
	/// Compare based on name only.
	/// </summary>
	internal sealed class OpaqueAssemblyNameComparer : IEqualityComparer<OpaqueAssembly>
	{
		public bool Equals(OpaqueAssembly x, OpaqueAssembly y)
		{
			const int ExactMatch = 0;
			bool same = string.Compare(
								x.Name,
								y.Name,
								StringComparison.OrdinalIgnoreCase)
								== ExactMatch;

			return same;
		}

		public bool Equals(OpaqueAssembly x, string y)
		{
			const int ExactMatch = 0;
			bool same = string.Compare(
								x.Name,
								y,
								StringComparison.OrdinalIgnoreCase)
								== ExactMatch;

			return same;
		}

		public int GetHashCode(OpaqueAssembly obj)
		{
			return obj.Name.GetHashCode();
		}
	}
}

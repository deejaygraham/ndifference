using System;
using System.Diagnostics;
using System.IO;

namespace NDifference
{
	/// <summary>
	/// Information about assembly on disk before reflection/introspection for types.
	/// </summary>
	[DebuggerDisplay("{Name}")]
	public class AssemblyDiskInfo : IAssemblyDiskInfo
	{
		private Identifier ident = new Identifier();
		
		public AssemblyDiskInfo()
		{
		}

		public AssemblyDiskInfo(string path)
			: this(path, DateTime.MinValue, 0, string.Empty)
		{
		}

		public AssemblyDiskInfo(string folder, string file)
			: this(System.IO.Path.Combine(folder, file))
		{
		}

		public AssemblyDiskInfo(string path, DateTime date, long size, string checksum)
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
		public static bool operator ==(AssemblyDiskInfo leftHandSide, AssemblyDiskInfo rightHandSide)
		{
			if (object.ReferenceEquals(leftHandSide, rightHandSide))
			{
				return true;
			}

			if (((object)leftHandSide == null) || ((object)rightHandSide == null))
			{
				return false;
			}

			return leftHandSide.PropertiesMatch(rightHandSide);
		}

		public static bool operator !=(AssemblyDiskInfo leftHandSide, AssemblyDiskInfo rightHandSide)
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

			AssemblyDiskInfo otherAssembly = obj as AssemblyDiskInfo;

			if ((object)otherAssembly == null)
			{
				return false;
			}

			return this.PropertiesMatch(otherAssembly);
		}

		public bool Equals(IAssemblyDiskInfo other)
		{
			if ((object)other == null)
			{
				return false;
			}

			return this.PropertiesMatch(other);
		}

		private bool PropertiesMatch(IAssemblyDiskInfo other)
		{
			return String.Equals(this.Name, other.Name, StringComparison.CurrentCultureIgnoreCase)
				&& this.Size == other.Size
				&& this.Date == other.Date
				&& this.Checksum == other.Checksum;
		}

		public string CalculateHash()
		{
			if (String.IsNullOrEmpty(this.Checksum))
			{
				this.Checksum = Guid.NewGuid().ToString();
			}

			return this.Checksum;
		}
	}
}

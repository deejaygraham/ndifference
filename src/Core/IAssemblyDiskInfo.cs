using System;

namespace NDifference
{
	/// <summary>
	/// Information about an assembly as it is on disk
	/// </summary>
	public interface IAssemblyDiskInfo : IUniquelyIdentifiable, IEquatable<IAssemblyDiskInfo>
	{
		/// <summary>
		/// File name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Full path to assembly
		/// </summary>
		string Path { get; }

		/// <summary>
		/// Date last modified
		/// </summary>
		DateTime Date { get; }

		/// <summary>
		/// Size in bytes
		/// </summary>
		long Size { get; }

		/// <summary>
		/// MD5 checksum
		/// </summary>
		string Checksum { get; }
	}
}

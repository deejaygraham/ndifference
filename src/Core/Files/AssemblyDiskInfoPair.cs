using System;

namespace NDifference
{
	/// <summary>
	/// Represents two versions of a single assembly.
	/// </summary>
	[Obsolete("Not used")]
	public class AssemblyDiskInfoPair : Pair<AssemblyDiskInfo>
	{
		public AssemblyDiskInfoPair()
			: base()
		{
		}

		public AssemblyDiskInfoPair(AssemblyDiskInfo firstPath, AssemblyDiskInfo secondPath)
			: base(firstPath, secondPath)
		{
		}

		public static AssemblyDiskInfoPair MakePair(string firstPath, string secondPath)
		{
			return new AssemblyDiskInfoPair(new AssemblyDiskInfo(firstPath), new AssemblyDiskInfo(secondPath));
		}
	}
}

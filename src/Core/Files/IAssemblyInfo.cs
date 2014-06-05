using System;
using System.Collections.ObjectModel;

namespace NDifference
{
	/// <summary>
	/// Information about internal assembly content.
	/// </summary>
	public interface IAssemblyInfo : IUniquelyIdentifiable
 	{
		/// <summary>
		/// Assembly name.
		/// </summary>
        string Name { get; }

		/// <summary>
		/// Assembly version.
		/// </summary>
        Version Version { get; }

		/// <summary>
		/// .Net runtime version.
		/// </summary>
		string RuntimeVersion { get; }

		/// <summary>
		/// x86|64|Any CPU etc.
		/// </summary>
        string Architecture { get;  }

		/// <summary>
		/// List of references to other assemblies.
		/// </summary>
		ReadOnlyCollection<AssemblyReference> References { get; }
	}
}
